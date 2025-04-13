namespace ICD.BookProject.Domain.View;

public class QuestionView
{
    public int Key { get; set; }
    public int QuestionnaireRef { get; set; }
    public string Text { get; set; }
    public string Type { get; set; }
    public int? OptionKey { get; set; }
    public string OptionText { get; set; }
    public string QuestionnaireTitle { get; set; }
}