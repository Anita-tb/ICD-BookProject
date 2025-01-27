using System.Collections.Generic;
using ICD.Framework.Model;

namespace ICD.BookProject;

public class AppendAuthorRequest : Request<AppendAuthorResult>
{
    public int BookId { get; set; }
    public List<int> AuthorIds { get; set; }
}
public class AppendAuthorResult : SingleQueryResult<AppendAuthorModel> {}
public class AppendAuthorModel {}
