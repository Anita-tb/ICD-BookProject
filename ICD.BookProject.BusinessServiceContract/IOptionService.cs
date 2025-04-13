using System.Threading.Tasks;

namespace ICD.BookProject.BusinessServiceContract;

public interface IOptionService
{
    Task<BaseOptionResult> InsertOptionAsync(InsertOptionRequest request);
    Task<BaseOptionResult> UpdateOptionAsync(UpdateOptionRequest request);
    Task<GetOptionsResult> GetOptionsAsync(GetOptionsQuery query);
    Task<DeleteTypeIntResult> DeleteOptionAsync(DeleteTypeIntRequest request);
    
}