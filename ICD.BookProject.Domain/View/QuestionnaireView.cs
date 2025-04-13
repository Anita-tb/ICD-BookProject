namespace ICD.BookProject.Domain.View;

public class QuestionnaireView
{
    public int Key { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public int? QuestionKey { get; set; }
    public string QuestionText { get; set; }
    public string QuestionType { get; set; }
    public int? OptionKey { get; set; }
    public string OptionText { get; set; }
    public int? ResponseKey { get; set; }
    public string Text { get; set; }
    public int? Digit { get; set; }
    public int? OptionRef { get; set; }
    
}