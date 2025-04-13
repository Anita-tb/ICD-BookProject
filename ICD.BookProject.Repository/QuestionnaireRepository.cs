using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ICD.BookProject.Domain.Entity;
using ICD.BookProject.Domain.View;
using ICD.BookProject.RepositoryContract;
using ICD.Framework.Data.Repository;
using ICD.Framework.DataAnnotation;
using ICD.Framework.Extensions;
using ICD.Framework.Model;
using ICD.Framework.QueryDataSource;

namespace ICD.BookProject.Repository;

[Dependency(typeof(IQuestionnaireRepository))]
public class QuestionnaireRepository : BaseRepository<QuestionnaireEntity,int> , IQuestionnaireRepository
{
    public async Task<ListQueryResult<QuestionnaireView>> GetQuestionnairesAsync(QueryDataSource<QuestionnaireView> queryDataSource)
    {
        var result = new ListQueryResult<QuestionnaireView>
        {
            Entities = new List<QuestionnaireView>()
        };
        
        var queryResult = from qe in GenericRepository.Query<QuestionnaireEntity>()
            join q in GenericRepository.Query<QuestionEntity>() on qe.Key equals q.QuestionnaireRef
            into leftQ
            from q in leftQ.DefaultIfEmpty()
            join o in GenericRepository.Query<OptionEntity>() on q.Key equals o.QuestionRef
            into leftO
            from o in leftO.DefaultIfEmpty()
            join r in GenericRepository.Query<ResponseEntity>() on q.Key equals r.QuestionRef
            into leftR
            from r in leftR.DefaultIfEmpty() 
            select new QuestionnaireView
            {
                Key = qe.Key,
                Title = qe.Title,
                Description = qe.Description,
                QuestionKey = q.Key,
                QuestionText = q.Text,
                QuestionType = q.Type,
                OptionKey = o.Key,
                OptionText = o.Text,
                Digit = r.Digit,
                Text = r.Text,
                OptionRef = r.OptionRef,
                ResponseKey = r.Key
            };
        
        result = await queryResult.ToListQueryResultAsync(queryDataSource);
        return result;
    }
}