using System.Threading.Tasks;

namespace ICD.BookProject.BusinessServiceContract;

public interface ILibraryService
{
    Task<AddBookResult> AddBookAsync(AddBookRequest request);
    Task<UpdateCurrentPageResult> UpdateCurrentPageAsync(UpdateCurrentPageRequest request);
    Task<GetMyBooksResult> GetMyBooksAsync(GetMyBooksQuery query);
    Task<DeleteTypeIntResult> RemoveBookAsync(DeleteTypeIntRequest request);
}