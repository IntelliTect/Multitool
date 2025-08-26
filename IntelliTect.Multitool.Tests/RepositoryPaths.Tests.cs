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
    public void BuildVariables_BeingSetProperly()
    {
        Assert.Equal(3, RepositoryPaths.BuildVariables.Count);

        Assert.True(RepositoryPaths.BuildVariables.TryGetValue("BuildingForLiveUnitTesting", out _));

        Assert.True(RepositoryPaths.BuildVariables.TryGetValue("ProjectPath", out string? projectPath));
        Assert.NotNull(projectPath);
        Assert.NotEmpty(projectPath);

        Assert.True(RepositoryPaths.BuildVariables.TryGetValue("SolutionDir", out string? solutionDir));
        Assert.NotNull(solutionDir);
        Assert.NotEmpty(solutionDir);
    }

    [Fact]
    public void TrySearchForGitContainingDirectory_ReturnsTrueWhenFound()
    {
        Assert.True(RepositoryPaths.TrySearchForGitContainingDirectory(new DirectoryInfo(Directory.GetCurrentDirectory()), out string gitParentDirectory));
        // Makes the assumption that the repository directory for this solution is named the same as the solution
        Assert.EndsWith(nameof(Multitool), gitParentDirectory);
    }

    [Fact]
    public void TrySearchForGitContainingDirectory_ReturnsFalseWhenNotFound()
    {
        string? path = Path.GetPathRoot(Directory.GetCurrentDirectory());
        Assert.NotNull(path);
        Assert.False(RepositoryPaths.TrySearchForGitContainingDirectory(new DirectoryInfo(path), out string gitParentDirectory));
        Assert.Empty(gitParentDirectory);
    }

    [Fact]
    public void BuildVariables_HandlesFileNotFound_Gracefully()
    {
        // Save current temp file if it exists
        string tempFilePath = Path.Combine(Path.GetTempPath(), RepositoryPaths.BuildVariableFileName);
        string? backupContent = null;
        bool fileExisted = File.Exists(tempFilePath);
        if (fileExisted)
        {
            backupContent = File.ReadAllText(tempFilePath);
            File.Delete(tempFilePath);
        }

        try
        {
            // Reset the static field to force re-initialization
            var field = typeof(RepositoryPaths).GetField("_buildVariables", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static);
            field?.SetValue(null, null);

            // Access BuildVariables when file doesn't exist - should not throw
            var buildVars = RepositoryPaths.BuildVariables;
            Assert.NotNull(buildVars);
            Assert.Empty(buildVars);
        }
        finally
        {
            // Restore the file if it existed
            if (fileExisted && backupContent != null)
            {
                File.WriteAllText(tempFilePath, backupContent);
            }
        }
    }
}