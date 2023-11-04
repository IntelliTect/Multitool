using System.Collections.ObjectModel;

namespace IntelliTect.Multitool;
/// <summary>
/// Provides consistent environment-independent normalized pathing within a repository.
/// </summary>
public static class RepositoryPaths
{
    /// <summary>
    /// Holds the key value pairs of the build variables that this class can use.
    /// </summary>
    public static readonly ReadOnlyDictionary<string, string?> BuildVariables = new(File.ReadAllLines(Path.Combine(Path.GetTempPath(), "IntelliTect.MultiTool.BuildVariables.tmp"))
        .Select(line => line.Split("::"))
        .ToDictionary(split => split[0].Trim(),
        split => !string.IsNullOrEmpty(split[1]) ? split[1].Trim() : null));

    /// <summary>
    /// Finds the root of the repository by looking for the .git folder.
    /// Defaults to the solution directory if available if the .git folder is not found.
    /// </summary>
    /// <returns>Full path to repo root.</returns>
    public static string GetDefaultRepoRoot()
    {
        DirectoryInfo? searchStartDirectory;
        if (!BuildVariables.TryGetValue("BuildingForLiveUnitTesting", out string? IsLiveUnitTesting)
            || IsLiveUnitTesting != "true")
        {
            searchStartDirectory = new(Directory.GetCurrentDirectory());
            return SearchForGitDirectory(ref searchStartDirectory);
        }
        else
        {
            if (BuildVariables.TryGetValue("ProjectPath", out string? projectPath))
            {
                searchStartDirectory = new FileInfo(projectPath).Directory;
                return SearchForGitDirectory(ref searchStartDirectory);
            }
            else
            {
                throw new InvalidOperationException("Could not find the repo root directory while in Live Unit Testing.");
            }
        }

    }

    private static string SearchForGitDirectory(ref DirectoryInfo? searchStartDirectory)
    {
        if (TrySearchForGitDirectory(ref searchStartDirectory, out string? gitDirectory) && !string.IsNullOrWhiteSpace(gitDirectory))
        {
            return gitDirectory;
        }
        else if (BuildVariables.TryGetValue("SolutionDir", out string? SolutionDir) && !string.IsNullOrWhiteSpace(SolutionDir))
        {
            return Directory.Exists(SolutionDir) ? SolutionDir : throw new InvalidOperationException("SolutionDir is not a valid directory.");
        }
        else
        {
            throw new InvalidOperationException("Could not find the repo root directory from the current directory. Current directory is expected to be the repoRoot sub directory.");
        }
    }

    private static bool TrySearchForGitDirectory(ref DirectoryInfo? searchStartDirectory, out string? gitDirectory)
    {
        while (searchStartDirectory is not null)
        {
            DirectoryInfo[] subDirectories = searchStartDirectory.GetDirectories(".git");
            if (subDirectories.Length > 0)
            {
                gitDirectory = searchStartDirectory.FullName;
                return true;
            }

            searchStartDirectory = searchStartDirectory.Parent;
        }
        gitDirectory = default;
        return false;
    }
}