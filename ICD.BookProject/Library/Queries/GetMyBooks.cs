using System.Collections.Generic;
using ICD.Framework.Model;

namespace ICD.BookProject;

public class GetMyBooksQuery : Query
{
    public int? Key { get; set; }
    public string AuthorName { get; set; }
    public string Title { get; set; }
    public string CategoryName { get; set; }
}
public class GetMyBooksResult : ListQueryResult<GetMyBooksModel>{}

public class GetMyBooksModel
{
    public int  Key { get; set; }
    public int BookRef{ get; set; }
    public string Title { get; set; }
    public string CategoryName { get; set; }
    public int Page { get; set; }
    public int CurrentPage { get; set; }
    public List<string> AuthorNames { get; set; }
    public string Progress { get; set; }
}