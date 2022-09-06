namespace IntelliTect.Multitool;
/// <summary>
/// Provides consistent environment-independent normalized pathing within a repository.
/// </summary>
public static class RepositoryPaths
{
    /// <summary>
    /// Finds the root of the repository by looking for the .git folder.
    /// </summary>
    /// <returns>Full path to repo root.</returns>
    public static string GetDefaultRepoRoot()
    {
        DirectoryInfo? currentDirectory = new(Directory.GetCurrentDirectory());

        while (currentDirectory is not null)
        {
            DirectoryInfo[] subDirectories = currentDirectory.GetDirectories(".git");
            if (subDirectories.Length > 0)
            {
                return currentDirectory.FullName;
            }

            currentDirectory = currentDirectory.Parent;
        }

        throw new InvalidOperationException("Could not find the repo root directory from the current directory. Current directory is expected to be the repoRoot sub directory.");
    }
}