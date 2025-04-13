using ICD.Framework.Model;

namespace ICD.BookProject;

public class BaseQuestionnaire : Request<BaseQuestionnaireResult>
{
    public string Title { get; set; }
    public string Description { get; set; }
}
public class BaseQuestionnaireResult : SingleQueryResult<BaseQuestionnaireModel> {}
public class BaseQuestionnaireModel{}