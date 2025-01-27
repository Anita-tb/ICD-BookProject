using ICD.Framework.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace ICD.BookProject.Domain.Entity
{
    public class BookEntity : BaseEntity<int>
    {
        public string Title { get; set; }
        public int Page { get; set; }

        #region Navigation Property
        
        public int? CategoryRef { get; set; }
        public CategoryEntity Category { get; set; }
        public ICollection<LibraryEntity> UserLibraries { get; set; }
        public ICollection<AuthorBookEntity> AuthorsBook { get; set; }
        
        #endregion
    }
}
