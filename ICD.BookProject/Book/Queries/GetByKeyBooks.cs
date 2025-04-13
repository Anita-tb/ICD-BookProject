using System.Collections.Generic;
using ICD.Framework.Model;


namespace ICD.BookProject;

public class GetByKeyBooksQuery : Query
{
    public int Key { get; set; }
}

public class GetByKeyBooksResult : SingleQueryResult<GetByKeyBooksModel>{}

public class GetByKeyBooksModel
{
    public int  Key { get; set; }
    public string Title { get; set; }
    //public string CategoryName { get; set; }
    //public int? CategoryRef { get; set; }
    public int Page { get; set; }
    //
    // public List<string> AuthorNames { get; set; } = new List<string>();
    // public List<int> AuthorRefs { get; set; } = new List<int>();
}