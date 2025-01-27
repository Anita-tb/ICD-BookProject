using System.Collections.Generic;
using System.Threading.Tasks;
using ICD.BookProject.Domain.Entity;
using ICD.BookProject.Domain.View;
using ICD.BookProject.RepositoryContract;
using ICD.Framework.Data.Repository;
using ICD.Framework.DataAnnotation;
using ICD.Framework.Model;
using ICD.Framework.QueryDataSource;
using System.Linq;
using ICD.Framework.Abstraction.Session;
using ICD.Framework.Extensions;

namespace ICD.BookProject.Repository;

[Dependency(typeof(IAuthorRepository))]
public class AuthorRepository : BaseRepository<AuthorEntity,int> , IAuthorRepository
{
    public async Task<ListQueryResult<AuthorView>> GetAuthorsAsync(QueryDataSource<AuthorView> queryDataSource)
    {
        var result = new ListQueryResult<AuthorView>
        {
            Entities = new List<AuthorView>()
        };

        var queryResult = from a in GenericRepository.Query<AuthorEntity>()
            /*join ub in GenericRepository.Query<UserBookEntity>() on u.Key equals ub.UserRef
            join b in GenericRepository.Query<BookEntity>() on ub.BookRef equals b.Key
            join ab in GenericRepository.Query<AuthorBookEntity>() on b.Key equals ab.BookRef 
            join a in GenericRepository.Query<AuthorEntity>() on ab.AuthorRef equals a.Key*/
            //where ub.UserRef == _appSession.UserRef
            select new AuthorView
            {
                Key = a.Key,
                Name = a.Name,
                //UserRef = ub.UserRef
            };
            
        result = await queryResult.ToListQueryResultAsync(queryDataSource);
        return result;
    }
}