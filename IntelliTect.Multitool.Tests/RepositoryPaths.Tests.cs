using Xunit;

namespace IntelliTect.Multitool.Tests;

public class RepositoryPathsTests
{
    [Fact]
    public void GetDefaultRepoRoot_ReturnsRepoRootDirectory()
    {
        // Makes the assumption that the repository directory for this solution is named the same as the solution
        Assert.EndsWith(nameof(Multitool), RepositoryPaths.GetDefaultRepoRoot());
    }

    [Fact]
    public void VariablesAreGettingSet()
    {
        string[] value = File.ReadAllLines(Path.Combine(Path.GetTempPath(), "IntelliTect.MultiTool.BuildVariables.tmp"));
        // Build a dictionary of the values and their associated keys from the file
        // Example: BuildingForLiveUnitTesting::null is a value of null for the key BuildingForLiveUnitTesting
        // If value is null or empty, set value to null in dictionary
        Dictionary<string, string?> dictionary = value
            .Select(line => line.Split("::"))
            .ToDictionary(
                           split => split[0],
                                          split => !string.IsNullOrEmpty(split[1]) ? split[1] : null);
        Assert.Equal(2, dictionary.Count);
        Assert.Null(dictionary["BuildingForLiveUnitTesting"]);
    }
}