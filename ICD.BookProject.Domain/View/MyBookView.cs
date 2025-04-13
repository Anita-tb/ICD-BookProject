namespace ICD.BookProject.Domain.View;

public class MyBookView
{
    public int  Key { get; set; }
    public int  BookRef { get; set; }
    public long UserRef { get; set; }
    public string Title { get; set; }
    public string CategoryName { get; set; }
    public int Page { get; set; }
    public int CurrentPage { get; set; }
    public string AuthorName { get; set; }
    public string Progress { get; set; }
}