using ICD.Framework.Domain;

namespace ICD.BookProject.Domain.Entity;

public class AuthorBookEntity : BaseEntity<int>
{
    #region Navigation Property
    public int AuthorRef { get; set; }
    public AuthorEntity Author { get; set; }
    public int  BookRef { get; set; }
    public BookEntity Book { get; set; }
    #endregion
}