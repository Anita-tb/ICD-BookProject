using System.Collections.Generic;
using ICD.Framework.Domain;

namespace ICD.BookProject.Domain.Entity;

public class CategoryEntity : BaseEntity<int>
{
    public string Name { get; set; }

    #region Navigation Property

    public ICollection<BookEntity> Books { get; set; }

    #endregion
}