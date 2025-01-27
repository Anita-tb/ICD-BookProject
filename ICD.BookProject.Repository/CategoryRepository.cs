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

[Dependency(typeof(ICategoryRepository))]
public class CategoryRepository : BaseRepository<CategoryEntity,int>, ICategoryRepository
{
    public async Task<ListQueryResult<CategoryView>> GetCategoriesAsync(QueryDataSource<CategoryView> queryDataSource)
    {
        var result = new ListQueryResult<CategoryView>
        {
            Entities = new List<CategoryView>()
        };
        var queryResult = from c in GenericRepository.Query<CategoryEntity>()
            select new CategoryView
            {
                Key = c.Key,
                Name = c.Name,
            };
            
        result = await queryResult.ToListQueryResultAsync(queryDataSource);
        return result;
    }
}