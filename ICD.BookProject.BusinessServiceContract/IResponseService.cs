using System.Threading.Tasks;

namespace ICD.BookProject.BusinessServiceContract;

public interface IResponseService
{
    Task<BaseResponseResult> InsertResponseAsync(InsertResponseRequest request);
    Task<GetResponsesResult> GetResponsesAsync(GetResponsesQuery query);
    Task<DeleteTypeIntResult> DeleteResponseAsync(DeleteTypeIntRequest request);
   
    
    
}