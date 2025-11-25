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
    public void TrySearchForSolutionContainingDirectory_ReturnsTrueWhenSlnFound()
    {
        Assert.True(RepositoryPaths.TrySearchForSolutionContainingDirectory(new DirectoryInfo(Directory.GetCurrentDirectory()), out string solutionDirectory));
        // Makes the assumption that the repository directory for this solution is named the same as the solution
        Assert.EndsWith(nameof(Multitool), solutionDirectory);
    }

    [Fact]
    public void TrySearchForSolutionContainingDirectory_ReturnsTrueWhenSlnxFound()
    {
        Assert.True(RepositoryPaths.TrySearchForSolutionContainingDirectory(new DirectoryInfo(Directory.GetCurrentDirectory()), out string solutionDirectory));
        // Makes the assumption that the repository directory for this solution is named the same as the solution
        Assert.EndsWith(nameof(Multitool), solutionDirectory);
    }

    [Fact]
    public void TrySearchForSolutionContainingDirectory_ReturnsFalseWhenNotFound()
    {
        string? path = Path.GetPathRoot(Directory.GetCurrentDirectory());
        Assert.NotNull(path);
        Assert.False(RepositoryPaths.TrySearchForSolutionContainingDirectory(new DirectoryInfo(path), out string solutionDirectory));
        Assert.Empty(solutionDirectory);
    }
}