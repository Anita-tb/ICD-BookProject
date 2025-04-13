using System.Collections.Generic;
using ICD.Framework.Model;

namespace ICD.BookProject;

public class GetQuestionnairesQuery : Query
{
    public int? Key { get; set; }
    public string QuestionType { get; set; }
    
}
public class GetQuestionnairesResult : ListQueryResult<GetQuestionnairesModel>{}

public class GetQuestionnairesModel
{
    public int  Key { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public List<QuestionInfo>? Questions { get; set; }
    
}

public class QuestionInfo
{
    public int QuestionKey { get; set; }
    public string QuestionText { get; set; }
    public string QuestionType { get; set; }
    public List<OptionInfo> Options { get; set; } 
    public ResponseInfo ResponseInfo { get; set; }
}


public class OptionInfo
{
    public int OptionKey { get; set; }
    public string OptionText { get; set; }
}

public class ResponseInfo
{
    public int ResponseKey { get; set; }
    public object Answer { get; set; }
}




