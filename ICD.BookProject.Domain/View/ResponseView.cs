namespace ICD.BookProject.Domain.View;

public class ResponseView
{
    public int Key { get; set; }
    public string OptionText { get; set; }
    public string Text { get; set; }
    public int? Digit { get; set; }
    public int? OptionRef { get; set; }
    public string QuestionText { get; set; }
    public string QuestionType { get; set; }
    public int QuestionKey { get; set; }
    public long UserRef { get; set; }
    public int QuestionnaireRef { get; set; }
    
}