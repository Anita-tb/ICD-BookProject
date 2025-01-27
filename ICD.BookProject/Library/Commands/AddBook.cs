using ICD.Framework.Model;

namespace ICD.BookProject;

public class AddBookRequest : Request<AddBookResult>
{
    public int BookId { get; set; }
    //public long UserId { get; set; }
    public int? CurrentPage { get; set; } = 0;
}
public class AddBookResult : SingleQueryResult<AddBookModel> {}
public class AddBookModel{}