using System;

using ICD.Framework.Model;

namespace ICD.BookProject;

public class BaseUser : Request<BaseUserResult>
{
    public string Username { get; set; }
    public string Password { get; set; }
    
}
public class BaseUserResult : SingleQueryResult<BaseUserModel>{}

public class BaseUserModel
{
    
}