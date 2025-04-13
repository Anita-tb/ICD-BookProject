using System.Threading.Tasks;

namespace ICD.BookProject.BusinessServiceContract;

public interface IQuestionService
{
    Task<BaseQuestionResult> InsertQuestionAsync(InsertQuestionRequest request);
    Task<GetQuestionsResult> GetQuestionsAsync(GetQuestionsQuery query);
    Task<BaseQuestionResult> UpdateQuestionAsync(UpdateQuestionRequest request);
    Task<DeleteTypeIntResult> DeleteQuestionAsync(DeleteTypeIntRequest request);
    
}