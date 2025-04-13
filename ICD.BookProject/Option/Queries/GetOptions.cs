using ICD.Framework.Model;

namespace ICD.BookProject;

public class GetOptionsQuery : Query
{
    public int? Key { get; set; }
    public int? QuestionRef { get; set; }
}
public class GetOptionsResult : ListQueryResult<GetOptionsModel> {}

public class GetOptionsModel
{
    public int Key { get; set; }
    public string Text { get; set; }
    public int QuestionRef { get; set; }
    public string QuestionText { get; set; }
}