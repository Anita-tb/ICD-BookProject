using System.Collections.Generic;
using ICD.Framework.Domain;

namespace ICD.BookProject.Domain.Entity;

public class UserEntity : BaseEntity<long>
{
    public string Username { get; set; }
    public string Password { get; set; }

    #region Navigation Property
    public ICollection<LibraryEntity> BookLibraries { get; set; }
    public ICollection<ResponseEntity> Responses { get; set; }
    
    #endregion
}