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

[Dependency(typeof(IResponseRepository))]
public class ResponseRepository : BaseRepository<ResponseEntity,int> , IResponseRepository
{
    public async Task<ListQueryResult<ResponseView>> GetResponsesAsync(QueryDataSource<ResponseView> queryDataSource)
    {
        var result = new ListQueryResult<ResponseView>
        {
            Entities = new List<ResponseView>()
        };
        
        var queryResult = from r in GenericRepository.Query<ResponseEntity>()
            join q in GenericRepository.Query<QuestionEntity>() on r.QuestionRef equals q.Key
            join o in GenericRepository.Query<OptionEntity>() on r.OptionRef equals o.Key 
                into leftO
            from o in leftO.DefaultIfEmpty()
            select new ResponseView
            {
                Key = r.Key,
                Text = r.Text,
                Digit = r.Digit,
                QuestionText = q.Text,
                QuestionType = q.Type,
                QuestionKey = r.QuestionRef,
                OptionText = o.Text,
                OptionRef = r.OptionRef,
                UserRef = r.UserRef,
                QuestionnaireRef = q.QuestionnaireRef,
            };
        
        result = await queryResult.ToListQueryResultAsync(queryDataSource);
        return result;
        
    }
}