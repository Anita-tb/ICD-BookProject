using ICD.Framework.Model;

namespace ICD.BookProject;

public class DeleteTypeLongRequest : Request<DeleteTypeLongResult>
{
    public long Key { get; set; }
}
public class DeleteTypeLongResult : SingleQueryResult<DeleteTypeLongModel> {}
public class DeleteTypeLongModel {}