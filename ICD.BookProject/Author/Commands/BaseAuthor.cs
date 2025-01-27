using ICD.Framework.Model;

namespace ICD.BookProject;

public class BaseAuthor : Request<BaseAuthorResult>
{
    public string Name { get; set; }
}
public class BaseAuthorResult : SingleQueryResult<BaseAuthorModel> {}
public class BaseAuthorModel {}
