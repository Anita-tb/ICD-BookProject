using ICD.Framework.Model;


namespace ICD.BookProject;

public class GetResponsesQuery : Query
{
    public int? Key { get; set; }
    public string QuestionType { get; set; }
    public int? QuestionKey { get; set; }
    public int? QuestionnaireKey { get; set; }
}
public class GetResponsesResult : ListQueryResult<GetResponsesModel>{}

public class GetResponsesModel
{
    public int Key { get; set; }
    public int QuestionnaireRef { get; set; }
    public int QuestionKey { get; set; }
    public string QuestionText { get; set; }
    public string QuestionType { get; set; }
    public string Text { get; set; }
    public int? Digit { get; set; }
    public int? OptionRef { get; set; }
    public string OptionText { get; set; }
}