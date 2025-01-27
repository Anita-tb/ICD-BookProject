using ICD.Framework.Model;

namespace ICD.BookProject;

public class AppendCategoryRequest : Request<AppendCategoryResult>
{
    public int BookId { get; set; }
    public int CategoryId { get; set; }
}
public class AppendCategoryResult : SingleQueryResult<AppendCategoryModel> {}
public class AppendCategoryModel {}