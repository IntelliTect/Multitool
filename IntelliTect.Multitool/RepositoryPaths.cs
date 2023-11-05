using System.Collections.ObjectModel;

namespace IntelliTect.Multitool;

/// <summary>
/// Provides consistent environment-independent normalized pathing within a repository.
/// </summary>
public static class RepositoryPaths
{
    /// <summary>
    /// Name of the build variables file that is created by the build process.
    /// </summary>
    public const string BuildVariableFileName = "IntelliTect.MultiTool.BuildVariables.tmp";

    /// <summary>
    /// Holds the key value pairs of the build variables that this class can use.
    /// </summary>
    public static ReadOnlyDictionary<string, string?> BuildVariables { get; } = new(File.ReadAllLines(Path.Combine(Path.GetTempPath(), BuildVariableFileName))
        .Select(line => line.Split("::"))
        .ToDictionary(split => split[0].Trim(),
        split => !string.IsNullOrEmpty(split[1]) ? split[1].Trim() : null));

    /// <summary>
    /// Finds the root of the repository by looking for the directory containing the .git directory.
    /// Begins searching up from the current directory, and retries from the project directory if initially not found.
    /// Defaults to the solution directory, if available, if the .git directory is not found.
    /// </summary>
    /// <returns>Full path to repo root.</returns>
    public static string GetDefaultRepoRoot()
    {
        string gitDirectory;
        DirectoryInfo? searchStartDirectory;

        // If not live unit testing, try searching from current directory. But if we are this will fail, so just skip
        if (!(BuildVariables.TryGetValue("BuildingForLiveUnitTesting", out string? isLiveUnitTesting)
            && isLiveUnitTesting == "true"))
        {
            searchStartDirectory = new(Directory.GetCurrentDirectory());
            if (TrySearchForGitContainingDirectory(searchStartDirectory, out gitDirectory)
                && !string.IsNullOrWhiteSpace(gitDirectory))
            {
                return gitDirectory;
            }
        }
        // Search from the project directory if we are live unit testing or if the initial search failed.
        if (BuildVariables.TryGetValue("ProjectPath", out string? projectPath))
        {
            searchStartDirectory = new FileInfo(projectPath).Directory;
            if (TrySearchForGitContainingDirectory(searchStartDirectory, out gitDirectory)
                && !string.IsNullOrWhiteSpace(gitDirectory))
            {
                return gitDirectory;
            }
        }
        // If all this fails, try returning the Solution Directory in hopes that is in the root of the repo.
        if (BuildVariables.TryGetValue("SolutionDir", out string? solutionDir) && !string.IsNullOrWhiteSpace(solutionDir))
        {
            return Directory.Exists(solutionDir) ? solutionDir : throw new InvalidOperationException($"SolutionDir is not a valid directory.");
        }
        throw new InvalidOperationException("Could not find the repo root directory from the current directory. Current directory is expected to be the repoRoot sub directory.");
    }

    /// <summary>
    /// Searches up from the <paramref name="searchStartDirectory"/> looking for a .git directory.
    /// </summary>
    /// <param name="searchStartDirectory">The directory to start searching from, will search up.</param>
    /// <param name="gitParentDirectory">The parent directory to the .git directory.</param>
    /// <returns><c>true</c> if the directory <paramref name="gitParentDirectory" /> was found successfully; otherwise, false.</returns>
    public static bool TrySearchForGitContainingDirectory(DirectoryInfo? searchStartDirectory, out string gitParentDirectory)
    {
        while (searchStartDirectory is not null)
        {
            DirectoryInfo[] subDirectories = searchStartDirectory.GetDirectories(".git");
            if (subDirectories.Length > 0)
            {
                gitParentDirectory = searchStartDirectory.FullName;
                return true;
            }

            searchStartDirectory = searchStartDirectory.Parent;
        } 
        gitParentDirectory = string.Empty;
        return false;
    }
}