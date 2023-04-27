using System.Text;

namespace IntelliTect.Multitool.Extensions;

/// <summary>
/// Various string extensions
/// </summary>
public static class StringExtensions
{
    /// <summary>
    /// Modify text so that it is URL compatible. IE: "Hi My Name is" -> "hi-my-name-is".
    /// Removes special characters, sets to lowercase, replaces ' ' and '_' with '-', and trims the string
    /// </summary>
    /// <param name="separatorCharacter">The character to use as a separator. Defaults to '-'.</param>
    /// <param name="str">The string to modify</param>
    /// <returns>A string that is url compatible</returns>
    public static string CreateUrlSlug(this string str, char separatorCharacter = '-')
    {
        StringBuilder sb = new();
        bool allowSeparator = false;
        char? nextSeparator = null;
        for (int i = 0; i < str.Length; i++)
        {
            char character = str[i];
            switch (character)
            {
                // this second '-' here is different than a normal - in terms of key code
                // so we replace it with a normal -
                case char c when c == '_' || c == ' ' || c == '–' || c == '-' || c == '.' || c == ',':
                    if (allowSeparator)
                    {
                        nextSeparator = separatorCharacter;
                        allowSeparator = false;
                    }
                    break;
                // Only allow letters and numbers as valid characters (removing things like diacritics (accents))
                case char c when (c >= '0' && c <= '9') || (c >= 'a' && c <= 'z') || (c >= 'A' && c <= 'Z'):
                    if (nextSeparator != null)
                    {
                        sb.Append(nextSeparator);
                        nextSeparator = null;
                    }
                    sb.Append(char.ToLowerInvariant(character));
                    allowSeparator = true;
                    break;
                default:
                    break;
            }
        }
        return sb.ToString();
    }

    /// <summary>
    /// Validates a URL string by checking to make sure the string is formatted correctly.
    /// </summary>
    /// <param name="hyperlink">The url string to check.</param>
    /// <returns>Result of validation.</returns>
    public static bool ValidateUrlString(this string hyperlink)
    {
        return Uri.IsWellFormedUriString(hyperlink, UriKind.Absolute) &&
            Uri.TryCreate(hyperlink, UriKind.Absolute, out _);
    }
}
