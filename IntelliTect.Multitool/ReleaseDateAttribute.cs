using System.Globalization;
using System.Reflection;

namespace IntelliTect.Multitool;

/// <summary>
/// The release date assembly attribute.
/// </summary>
[AttributeUsage(AttributeTargets.Assembly)]
public class ReleaseDateAttribute : Attribute
{
    /// <summary> 
    /// Constructor that takes in a DateTime string
    /// </summary>
    /// <param name="utcDateString">A DateTime 'O' (round-trip date/time) format string</param>
    public ReleaseDateAttribute(string utcDateString)
    {
        ReleaseDate = DateTime.ParseExact(utcDateString, "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind);
    }

    /// <summary>
    /// The date the assembly was built
    /// </summary>
    public DateTime ReleaseDate { get; }

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