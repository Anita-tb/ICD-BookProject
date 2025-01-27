using ICD.Framework.Model;

namespace ICD.BookProject;

public class GetCategoriesQuery : Query
{
    public int? Key { get; set; }
    public string Name { get; set; }
}
public class GetCategoriesResult : ListQueryResult<GetCategoriesModel> {}

public class GetCategoriesModel
{
    public int Key { get; set; }
    public string Name { get; set; }
}