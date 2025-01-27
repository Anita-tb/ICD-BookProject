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

[Dependency(typeof(IUserRepository))]
public class UserRepository : BaseRepository<UserEntity,long>, IUserRepository
{
    public async Task<ListQueryResult<UserView>> GetUsersAsync(QueryDataSource<UserView> queryDataSource)
    {
        
        var result = new ListQueryResult<UserView>
        {
            Entities = new List<UserView>()
        };

        var queryResult = from u in GenericRepository.Query<UserEntity>()
            select new UserView
            {
                Key = u.Key,
                Password = u.Password,
                Username = u.Username,
            };
            
        result = await queryResult.ToListQueryResultAsync(queryDataSource);
        return result;
        
    }
}