using System.Collections.Generic;
using ICD.Framework.Domain;

namespace ICD.BookProject.Domain.Entity;

public class AuthorEntity : BaseEntity<int>
{
    public string Name { get; set; }
    #region Navigation Property
    public ICollection<AuthorBookEntity> AuthorBooks { get; set; }
    #endregion
}