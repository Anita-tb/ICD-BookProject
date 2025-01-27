using ICD.Framework.Model;

namespace ICD.BookProject;

public class BaseCategory : Request<BaseCategoryResult>
{
    public string Name { get; set; }
}
public class BaseCategoryResult : SingleQueryResult<BaseCategoryModel> {}
public class BaseCategoryModel {}