using ICD.Framework.Model;

namespace ICD.BookProject;

public class GetUsersQuery : Query
{
    public long? Key { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
}

public class GetUsersResult : ListQueryResult<GetUsersModel>{}

public class GetUsersModel
{
    public long Key { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
   
}