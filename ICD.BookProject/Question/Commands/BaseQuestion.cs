using ICD.Framework.Model;

namespace ICD.BookProject;

public class BaseQuestion : Request<BaseQuestionResult>
{
    public string Type { get; set; }
    public string Text { get; set; }
    public int QuestionnaireRef { get; set; }
}
public class BaseQuestionResult : SingleQueryResult<BaseQuestionModel> {}
public class BaseQuestionModel {}