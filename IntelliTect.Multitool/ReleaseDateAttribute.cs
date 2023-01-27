using System.Globalization;
using System.Reflection;

namespace IntelliTect.Multitool;

/// <summary>
/// Information about the executing assembly.
/// </summary>
[AttributeUsage(AttributeTargets.Assembly)]
public class ReleaseDateAttribute : Attribute
{
    /// <summary> 
    /// Constructor called from csproj file
    /// </summary>
    /// <param name="utcDateString">A utc O (round-trip date/time) format string</param>
    public ReleaseDateAttribute(string utcDateString)
    {
        ReleaseDate = DateTime.ParseExact(utcDateString, "O", CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal);
    }
    /// <summary>
    /// The date the assembly was built
    /// </summary>
    public DateTime ReleaseDate { get; }
    /// <summary>
    /// Method to obtain the release date from the assembly attributes
    /// </summary>
    /// <param name="assembly">An assembly instance</param>
    /// <returns>The date time from compilation time</returns>
    public static DateTime? GetReleaseDate(Assembly? assembly = null)
    {
        object[]? attribute = (assembly ?? Assembly.GetEntryAssembly())?.GetCustomAttributes(typeof(ReleaseDateAttribute), false);
        return attribute?.Length >= 1 ? ((ReleaseDateAttribute)attribute[0]).ReleaseDate : null;
    }
}