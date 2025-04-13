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

[Dependency(typeof(IQuestionRepository))]
public class QuestionRepository : BaseRepository<QuestionEntity,int> , IQuestionRepository
{
    public async Task<ListQueryResult<QuestionView>> GetQuestionsAsync(QueryDataSource<QuestionView> queryDataSource)
    {
        var result = new ListQueryResult<QuestionView>
        {
            Entities = new List<QuestionView>()
        };
        
        var queryResult = from q in GenericRepository.Query<QuestionEntity>()
            join qe in GenericRepository.Query<QuestionnaireEntity>() on q.QuestionnaireRef equals qe.Key
            join o in GenericRepository.Query<OptionEntity>() on q.Key equals o.QuestionRef
                into leftO
            from o in leftO.DefaultIfEmpty()
            select new QuestionView
            {
                Key = q.Key,
                Text = q.Text,
                Type = q.Type,
                QuestionnaireRef = q.QuestionnaireRef,
                QuestionnaireTitle = qe.Title,
                OptionKey = o.Key,
                OptionText = o.Text
            };
        
        result = await queryResult.ToListQueryResultAsync(queryDataSource);
        return result;
        
    }
}