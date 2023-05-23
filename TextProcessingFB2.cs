using System.Text.RegularExpressions;
class TextProcessingFB2
{
    public void Processing(string path)
    {
        string text = File.ReadAllText(path);
        Regex regex = new Regex(@"\<(\d|\w|-|:|/|\.| |=|;|,|\?)*\>");

        text = regex.Replace(text, " ");
        text = text.ToLower();
        File.WriteAllText(@"War_and_peace_buffer.txt", text);
    }
}