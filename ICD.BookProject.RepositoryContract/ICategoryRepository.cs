using System.Threading.Tasks;
using ICD.BookProject.Domain.Entity;
using ICD.BookProject.Domain.View;
using ICD.Framework.Data.Repository;
using ICD.Framework.Model;
using ICD.Framework.QueryDataSource;

namespace ICD.BookProject.RepositoryContract;

public interface ICategoryRepository : IRepository<CategoryEntity,int>
{
    Task<ListQueryResult<CategoryView>> GetCategoriesAsync(QueryDataSource<CategoryView> queryDataSource);
}