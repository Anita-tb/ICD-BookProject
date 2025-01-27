using ICD.Framework.Domain;

namespace ICD.BookProject.Domain.Entity;

public class LibraryEntity : BaseEntity<int>
{
   public int CurrentPage { get; set; }
   #region Navigation Property

   public int BookRef { get; set; }
   public BookEntity Book { get; set; }
   public long UserRef { get; set; }
   public UserEntity User { get; set; }

   #endregion
}