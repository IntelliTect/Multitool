using System.Text;

namespace IntelliTect.Multitool.Extensions;

/// <summary>
/// Various string extensions
/// </summary>
public static class StringExtensions
{
    /// <summary>
    /// Slugify's text so that it is URL compatible. IE: "Hi My Name is" -> "hi-my-name-is".
    /// Removes special characters, sets to lowercase, replaces ' ' and '_' with '-', and trims the string
    /// </summary>
    public static string Slugify(this string str)
    {
        str = str.ToLowerInvariant().Trim();
        StringBuilder sb = new();
        const char separatorCharacter = '-';
        bool allowSeparator = false;
        foreach (char character in str)
        {
            switch (character)
            {
                // this second '-' here is different than a normal - in terms of key code
                // so we replace it with a normal -
                case char c when c == '_' || c == ' ' || c == '–' || c == '-' || c == '.' || c == ',':
                    if (allowSeparator)
                    {
                        sb.Append(separatorCharacter);
                        allowSeparator = false;
                    }
                    break;
                // Only allow letters and numbers as valid characters (removing things like diacritics (accents))
                case char c when (c >= '0' && c <= '9') || (c >= 'a' && c <= 'z'):
                    sb.Append(character);
                    allowSeparator = true;
                    break;
                default:
                    break;
            }
        }
        return sb.ToString().TrimEnd(separatorCharacter);
    }

    /// <summary>
    /// Validates a URL string by checking to make sure the string is formatted correctly
    /// and by attempting to make a GET request to it.
    /// </summary>
    /// <param name="hyperlink"></param>
    /// <param name="logUpdates"></param>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException"></exception>
    public static async Task<bool> ValidateUrlString(this string hyperlink, bool logUpdates = true)
    {
        if (Uri.IsWellFormedUriString(hyperlink, UriKind.Absolute) &&
            Uri.TryCreate(hyperlink, UriKind.Absolute, out Uri? uri))
        {
            if (!await uri.ValidateUri())
            {
                if (logUpdates) throw new InvalidOperationException($"HTTP Request Check Failed for HyperLink URL=({hyperlink})");
                return false;
            }
            return true;
        }
        else { return false; }
    }
}
