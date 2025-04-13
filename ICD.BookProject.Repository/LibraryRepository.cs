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

[Dependency(typeof(ILibraryRepository))]
public class LibraryRepository : BaseRepository<LibraryEntity,int> , ILibraryRepository
{
    public async Task<ListQueryResult<MyBookView>> GetMyBooksAsync(QueryDataSource<MyBookView> queryDataSource)
    {
        var result = new ListQueryResult<MyBookView>
        {
            Entities = new List<MyBookView>()
        };
        
        var queryResult = from u in GenericRepository.Query<UserEntity>()
            join l in GenericRepository.Query<LibraryEntity>() on u.Key equals l.UserRef
            join b in GenericRepository.Query<BookEntity>() on l.BookRef equals b.Key
            join ab in GenericRepository.Query<AuthorBookEntity>() on b.Key equals ab.BookRef
            join a in GenericRepository.Query<AuthorEntity>() on ab.AuthorRef equals a.Key
            join c in GenericRepository.Query<CategoryEntity>() on b.CategoryRef equals c.Key 
            select new MyBookView
            {
                Key = l.Key,
                Title = b.Title,
                CategoryName = c.Name,
                Page = b.Page,
                AuthorName = a.Name,
                CurrentPage = l.CurrentPage,
                UserRef = l.UserRef,
                BookRef = l.BookRef,
                Progress = b.Page > 0 ? $"{((double)l.CurrentPage / b.Page * 100):F2}%" : "0%"

                /*AuthorNames = new List<string>
                {
                    ab.Author.AuthorName
                }*/
            };
        
        result = await queryResult.ToListQueryResultAsync(queryDataSource);
        return result;
        
    }
}