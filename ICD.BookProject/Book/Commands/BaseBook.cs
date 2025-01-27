using ICD.Framework.Model;

namespace ICD.BookProject;

public class BaseBook : Request<BaseBookResult>
{
    public string Title { get; set; }
    //public int? AuthorId { get; set; }
    public int  Page { get; set; }
    
}
public class BaseBookResult : SingleQueryResult<BaseBookModel>{}
public class BaseBookModel{}