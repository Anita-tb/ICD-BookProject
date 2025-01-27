using ICD.Framework.Model;

namespace ICD.BookProject;

public class UpdateCurrentPageRequest : Request<UpdateCurrentPageResult>
{
    public int Key { get; set; }
    public int CurrentPage { get; set; }
}
public class UpdateCurrentPageResult : SingleQueryResult<UpdateCurrentPageModel> {}
public class UpdateCurrentPageModel {}
