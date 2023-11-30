using System.Globalization;
using System.Reflection;

namespace IntelliTect.Multitool;

/// <summary>
/// The release date assembly attribute with a constructor that takes in a DateTime string
/// </summary>
/// <param name="utcDateString">A DateTime 'O' (round-trip date/time) format string</param>
[AttributeUsage(AttributeTargets.Assembly)]
public class ReleaseDateAttribute(string utcDateString) : Attribute
{
    /// <summary>
    /// The date the assembly was built
    /// </summary>
    public DateTime ReleaseDate { get; } = DateTime.ParseExact(utcDateString, "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind);

    /// <summary>
    /// Method to obtain the release date from the assembly attribute
    /// </summary>
    /// <param name="assembly">An assembly instance</param>
    /// <returns>The date time from the assembly attribute</returns>
    public static DateTime? GetReleaseDate(Assembly? assembly = null)
    {
        object[]? attribute = (assembly ?? Assembly.GetEntryAssembly())?.GetCustomAttributes(typeof(ReleaseDateAttribute), false);
        return attribute?.Length >= 1 ? ((ReleaseDateAttribute)attribute[0]).ReleaseDate : null;
    }

}
