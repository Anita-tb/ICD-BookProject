using System.Threading.Tasks;

namespace ICD.BookProject.BusinessServiceContract;

public interface IAuthorService
{
    Task<BaseAuthorResult> InsertAuthorAsync(InsertAuthorRequest request);
    Task<BaseAuthorResult> UpdateAuthorAsync(UpdateAuthorRequest request);
    Task<GetAuthorsResult> GetAuthorsAsync(GetAuthorsQuery query);
    Task<DeleteTypeIntResult> DeleteAuthorAsync(DeleteTypeIntRequest request);
    
}