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
    public static string buildVariableFileName = "IntelliTect.MultiTool.BuildVariables.tmp";

    /// <summary>
    /// Holds the key value pairs of the build variables that this class can use.
    /// </summary>
    public static readonly ReadOnlyDictionary<string, string?> BuildVariables = new(File.ReadAllLines(Path.Combine(Path.GetTempPath(), buildVariableFileName))
        .Select(line => line.Split("::"))
        .ToDictionary(split => split[0].Trim(),
        split => !string.IsNullOrEmpty(split[1]) ? split[1].Trim() : null));

    /// <summary>
    /// Finds the root of the repository by looking for the .git folder.
    /// Begins by searching from the current directory, and trying again from the project directory if not found
    /// Defaults to the solution directory if available if the .git folder is not found.
    /// </summary>
    /// <returns>Full path to repo root.</returns>
    public static string GetDefaultRepoRoot()
    {
        string? gitDirectory;
        DirectoryInfo? searchStartDirectory;

        // If not live unit testing, try searching from current directory. But if we are this will fail, so just skip
        if (!(BuildVariables.TryGetValue("BuildingForLiveUnitTesting", out string? IsLiveUnitTesting)
            && IsLiveUnitTesting == "true"))
        {
            searchStartDirectory = new(Directory.GetCurrentDirectory());
            if (TrySearchForGitDirectory(searchStartDirectory, out gitDirectory) && !string.IsNullOrWhiteSpace(gitDirectory))
            {
                return gitDirectory;
            }
        }
        // Search from the project directory if we are live unit testing or if the initial search failed.
        if (BuildVariables.TryGetValue("ProjectPath", out string? projectPath))
        {
            searchStartDirectory = new FileInfo(projectPath).Directory;
            if (TrySearchForGitDirectory(searchStartDirectory, out gitDirectory) && !string.IsNullOrWhiteSpace(gitDirectory))
            {
                return gitDirectory;
            }
        }
        // If all this fails, try returning the Solution Directory in hopes that is in the root of the repo.
        if (BuildVariables.TryGetValue("SolutionDir", out string? SolutionDir) && !string.IsNullOrWhiteSpace(SolutionDir))
        {
            return Directory.Exists(SolutionDir) ? SolutionDir : throw new InvalidOperationException("SolutionDir is not a valid directory.");
        }
        throw new InvalidOperationException("Could not find the repo root directory from the current directory. Current directory is expected to be the repoRoot sub directory.");
    }

    private static bool TrySearchForGitDirectory(DirectoryInfo? searchStartDirectory, out string? gitDirectory)
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