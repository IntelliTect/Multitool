namespace IntelliTect.Multitool;
/// <summary>
/// Provides normalized paths.
/// </summary>
public interface IPaths
{
    /// <summary>
    /// Returns the repository root directory name.
    /// </summary>
    /// <param name="CurrentProjectFolderName">The highest level directory name of the current project within the repository.</param>
    /// <returns></returns>
    public static string GetDefaultRepoRoot(string CurrentProjectFolderName)
    {
        DirectoryInfo? currentDirectory = new(Directory.GetCurrentDirectory());

        while (currentDirectory is not null)
        {
            if (currentDirectory.FullName.ToLowerInvariant().EndsWith(CurrentProjectFolderName.ToLowerInvariant(), StringComparison.InvariantCulture))
            {
                DirectoryInfo? parent = currentDirectory.Parent;
                if (parent is not null)
                {
                    return parent.FullName;
                }
            }

            currentDirectory = currentDirectory.Parent;
        }

        throw new InvalidOperationException("Could not find the repo root directory from the current directory. Current directory is expected to be the repoRoot sub directory.");
    }
}