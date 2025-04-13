using System.Threading.Tasks;

namespace ICD.BookProject.BusinessServiceContract;

public interface IQuestionnarieService
{
    Task<GetQuestionnairesResult> GetQuestionnairesAsync(GetQuestionnairesQuery query);
    Task<BaseQuestionnaireResult> InsertQuestionnaireAsync(InsertQuestionnaireRequest request);
    Task<BaseQuestionnaireResult> UpdateQuestionnaireAsync(UpdateQuestionnaireRequest request);
    Task<DeleteTypeIntResult> DeleteQuestionnaireAsync(DeleteTypeIntRequest request);
    
}