using System.Collections.Generic;

namespace ICD.BookProject;

public class InsertBookRequest : BaseBook
{
    public List<int?> AuthorIds { get; set; }
    public int? CategoryRef { get; set; }
}