using System.Collections.Generic;
using ICD.Framework.Model;

namespace ICD.BookProject;

public class GetQuestionsQuery : Query 
{
    public int? Key { get; set; }
    public string Type { get; set; }
}
public class GetQuestionsResult : ListQueryResult<GetQuestionsModel>{}

public class GetQuestionsModel
{
    public int  Key { get; set; }
    public string Text { get; set; }
    public string Type { get; set; }
    public int QuestionnaireRef { get; set; }
    public string QuestionnaireTitle { get; set; }
    public List<OptionInfo> Options { get; set; }
}
