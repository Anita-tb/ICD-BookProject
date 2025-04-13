using ICD.Framework.Data.Repository;
using ICD.Framework.DataAnnotation;
using ICD.BookProject.Domain.Entity;
using ICD.BookProject.RepositoryContract;
using System;
using System.Collections.Generic;
using ICD.Framework.Model;
using ICD.Framework.QueryDataSource;
using ICD.Framework.QueryDataSource.Sort;
using ICD.Framework;
using System.Linq;
using System.Threading.Tasks;
using ICD.BookProject.Domain.View;
using ICD.Framework.Abstraction.Session;
using ICD.Framework.Extensions;
using ICD.Framework.AppMapper.Extensions;

namespace ICD.BookProject.Repository
{
    [Dependency(typeof(IBookRepository))]
    public class BookRepository : BaseRepository<BookEntity, int>, IBookRepository
    {
        public async Task<ListQueryResult<BookView>> GetBooksAsync(QueryDataSource<BookView> queryDataSource)
        {
            var result = new ListQueryResult<BookView>
            {
                Entities = new List<BookView>()
            };

            var queryResult = from b in GenericRepository.Query<BookEntity>() 
                join c in GenericRepository.Query<CategoryEntity>() on b.CategoryRef equals c.Key
                into leftC 
                from c in leftC.DefaultIfEmpty()
                join ab in GenericRepository.Query<AuthorBookEntity>() on b.Key equals ab.BookRef
                into leftAb
                from ab in leftAb.DefaultIfEmpty()
                join a in GenericRepository.Query<AuthorEntity>() on ab.AuthorRef equals a.Key
                into leftA
                from a in leftA.DefaultIfEmpty()
                select new BookView
                {
                    Key = b.Key,
                    Title = b.Title,
                    CategoryRef = b.CategoryRef,
                    CategoryName = c.Name,
                    Page = b.Page,
                    AuthorName = a.Name,
                    AuthorRef = a.Key,
                    /*AuthorNames = new List<string>
                    {
                        ab.Author.AuthorName
                    }*/
                };
            
            /*var queryResult = from b in GenericRepository.Query<BookEntity>()
                                 select new BookView
                                 {
                                     Key = b.Key,
                                     Title = b.Title,
                                     Category = b.Category,
                                     Page = b.Page,
                                     CurrentPage = b.CurrentPage,
                                     AuthorNames = (
                                     from ab in GenericRepository.Query<AuthorBookEntity>().Where(x => b.Key == x.BookRef)
                                     join a in GenericRepository.Query<AuthorEntity>() on ab.AuthorRef equals a.Key
                                     select a.AuthorName
                                                    ).ToList()
                                 };;*/
            

            
            result = await queryResult.ToListQueryResultAsync(queryDataSource);
            return result;
        }
    }
}
