using ICD.Framework.Model;

namespace ICD.BookProject;

public class DeleteTypeIntRequest : Request<DeleteTypeIntResult>
{
    public int Key { get; set; }
}
public class DeleteTypeIntResult : SingleQueryResult<DeleteTypeIntModel> {}
public class DeleteTypeIntModel { }