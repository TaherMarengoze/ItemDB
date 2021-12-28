using System.Text.RegularExpressions;

class TextControlling
{

    private string RemoveInvalidCharactersFromID(string inputId)
    {
        return Regex.Replace(inputId, "[^A-Z0-9]+", "", RegexOptions.Compiled);
    }

    private string ModifyInputToPattern(string text, string pattern)
    {
        return Regex.Replace(text, pattern, "", RegexOptions.Compiled);
    }
}