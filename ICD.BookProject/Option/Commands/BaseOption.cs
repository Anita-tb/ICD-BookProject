using ICD.Framework.Model;

namespace ICD.BookProject;

public class BaseOption : Request<BaseOptionResult>
{
    public string Text { get; set; }
    public int QuestionRef { get; set; }
}
public class BaseOptionResult : SingleQueryResult<BaseOptionModel> {}
public class BaseOptionModel {}