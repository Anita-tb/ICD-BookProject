using System.Collections.Generic;
using ICD.Framework.Model;

namespace ICD.BookProject;

public class GetBooksQuery : Query
{
    public int? Key { get; set; }
    public string AuthorName { get; set; }
    public string Title { get; set; }
    public string CategoryName { get; set; }
    
}

public class GetBooksResult : ListQueryResult<GetBooksModel>{}

public class GetBooksModel
{
    public int  Key { get; set; }
    public string Title { get; set; }
    public string CategoryName { get; set; }
    public int? CategoryRef { get; set; }
    public int Page { get; set; }
    //public int? CurrentPage { get; set; }
    public List<string> AuthorNames { get; set; } = new List<string>();
    public List<int> AuthorRefs { get; set; } = new List<int>();
}