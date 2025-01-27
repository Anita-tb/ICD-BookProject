using ICD.Framework.Model;

namespace ICD.BookProject;

public class LoginUserRequest : Request<LoginUserResult>
{
    public string Username { get; set; }
    public string Password { get; set; }
    
}
public class LoginUserResult : SingleQueryResult<LoginUserModel>{}

public class LoginUserModel
{
    public string Token { get; set; }
}