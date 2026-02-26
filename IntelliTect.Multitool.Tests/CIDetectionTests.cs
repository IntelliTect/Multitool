using System.IO;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using System.Xml;
using Microsoft.Build.Construction;
using Microsoft.Build.Evaluation;
using Xunit;

namespace IntelliTect.Multitool.Tests;

[Collection(MSBuildCollection.CollectionName)]
public class CIDetectionTests
{
    private static readonly string TargetsPath = Path.Combine(
        RepositoryPaths.GetDefaultRepoRoot(),
        "IntelliTect.Multitool", "Build", "IntelliTect.Multitool.targets");

    // Derived from the targets file itself — automatically stays in sync when new CI variables are added.
    private static readonly Lazy<IReadOnlyCollection<string>> _ciVarNames = new(() =>
    {
        var doc = XDocument.Load(TargetsPath);
        var conditions = doc.Descendants()
            .Where(e => e.Name.LocalName == "CI" && e.Attribute("Condition") != null)
            .Select(e => e.Attribute("Condition")!.Value);
        var names = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        foreach (string condition in conditions)
            foreach (Match m in Regex.Matches(condition, @"\$\((\w+)\)"))
                names.Add(m.Groups[1].Value);
        return names;
    });

    [Theory]
    [InlineData("GITHUB_ACTIONS", "true")]
    [InlineData("GITLAB_CI", "true")]
    [InlineData("CIRCLECI", "true")]
    [InlineData("CONTINUOUS_INTEGRATION", "true")]
    [InlineData("TF_BUILD", "true")]
    [InlineData("TEAMCITY_VERSION", "1.0")]
    [InlineData("APPVEYOR", "True")]
    [InlineData("BuildRunner", "MyGet")]
    [InlineData("JENKINS_URL", "http://jenkins")]
    [InlineData("TRAVIS", "true")]
    [InlineData("BUDDY", "true")]
    [InlineData("CODEBUILD_CI", "true")]
    public void CiEnvVar_SetsCIPropertyToTrue(string envVar, string value)
    {
        string ci = EvaluateCIProperty(new Dictionary<string, string> { [envVar] = value });
        Assert.Equal("true", ci);
    }

    [Fact]
    public void NoCiEnvVars_SetsCIPropertyToFalse()
    {
        string ci = EvaluateCIProperty([]);
        Assert.Equal("false", ci);
    }

    [Fact]
    public void CiAlreadyTrue_IsNotOverridden()
    {
        string ci = EvaluateCIProperty(new Dictionary<string, string> { ["CI"] = "true" });
        Assert.Equal("true", ci);
    }

    private static string EvaluateCIProperty(Dictionary<string, string> overrides)
    {
        // Clear all CI-related variables parsed from the targets file so that any env vars
        // set on the host runner (e.g. GITHUB_ACTIONS=true on GitHub Actions) don't leak in.
        // Global properties override process env vars in MSBuild evaluation.
        var globalProperties = _ciVarNames.Value
            .ToDictionary(v => v, _ => "", StringComparer.OrdinalIgnoreCase);
        foreach (var (key, value) in overrides)
            globalProperties[key] = value;

        using var collection = new ProjectCollection(globalProperties);
        string xml = $"""
            <Project>
              <Import Project="{TargetsPath.Replace(@"\", "/")}" />
            </Project>
            """;
        using var reader = XmlReader.Create(new StringReader(xml));
        ProjectRootElement rootElement = ProjectRootElement.Create(reader, collection);
        var project = new Project(rootElement, null, null, collection);
        return project.GetPropertyValue("CI");
    }
}
