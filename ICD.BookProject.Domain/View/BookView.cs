using System.Collections;
using System.Collections.Generic;

namespace ICD.BookProject.Domain.View;

public class BookView
{
    public int  Key { get; set; }
    public string Title { get; set; }
    public string CategoryName { get; set; }
    public int? CategoryRef { get; set; }
    public int Page { get; set; }
    //public int? CurrentPage { get; set; }
    public string AuthorName { get; set; }
    
}