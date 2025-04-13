using ICD.Framework.Model;

namespace ICD.BookProject;

public class BaseResponse : Request<BaseResponseResult>
{
    public int QuestionKey { get; set; }
    public object Answer { get; set; }
}
public class BaseResponseResult : SingleQueryResult<BaseResponseModel> {}
public class BaseResponseModel {}