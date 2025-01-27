using ICD.Framework.Model;

namespace ICD.BookProject;

public class GetAuthorsQuery : Query
{
    public int? Key { get; set; }
    public string Name { get; set; }
}
public class GetAuthorsResult : ListQueryResult<GetAuthorsModel> {}

public class GetAuthorsModel
{
    public int Key { get; set; }
    public string Name { get; set; }
}