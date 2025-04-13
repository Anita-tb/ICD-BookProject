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

[Dependency(typeof(IOptionRepository))]
public class OptionRepository : BaseRepository<OptionEntity,int> , IOptionRepository
{
    public async Task<ListQueryResult<Optionview>> GetOptionsAsync(QueryDataSource<Optionview> queryDataSource)
    {
        var result = new ListQueryResult<Optionview>
        {
            Entities = new List<Optionview>()
        };
        
        var queryResult = from o in GenericRepository.Query<OptionEntity>()
            join q in GenericRepository.Query<QuestionEntity>() on o.QuestionRef equals q.Key
            select new Optionview
            {
                Key = o.Key,
                Text = o.Text,
                QuestionText = q.Text,
                QuestionRef = o.QuestionRef,
            };
        
        result = await queryResult.ToListQueryResultAsync(queryDataSource);
        return result;
    }
}