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
        
        Assert.True(RepositoryPaths.BuildVariables.TryGetValue("ProjectPath", out string? ProjectPath));
        Assert.NotNull(ProjectPath);
        Assert.NotEmpty(ProjectPath);
        
        Assert.True(RepositoryPaths.BuildVariables.TryGetValue("SolutionDir", out string? SolutionDir));
        Assert.NotNull(SolutionDir);
        Assert.NotEmpty(SolutionDir);
    }
}