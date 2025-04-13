using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ICD.Framework.Data.UnitOfWork;
using ICD.Framework.DataAnnotation;
using ICD.BookProject.BusinessServiceContract;
using ICD.BookProject.Domain.Entity;
using ICD.BookProject.Domain.View;
using ICD.BookProject.RepositoryContract;
using ICD.Framework.Abstraction.Session;
using ICD.Framework.AppMapper.Extensions;
using ICD.Framework.Extensions;
using ICD.Framework.QueryDataSource;
using ICD.Framework.QueryDataSource.Fitlter;


namespace ICD.BookProject.BusinessService
{
    
    [Dependency(typeof(IBookService))]
    
    public class BookService : IBookService
    {
        private readonly IUnitOfWork _db;
        private readonly IBookRepository _bookRepository;
        private readonly IAuthorRepository _authorRepository;
        private readonly IAuthorBookRepository _authorBookRepository;
        
        

        public BookService(IUnitOfWork db)
        {
            _db = db;
            _bookRepository = _db.GetRepository<IBookRepository>();
            _authorRepository = _db.GetRepository<IAuthorRepository>();
            _authorBookRepository = _db.GetRepository<IAuthorBookRepository>();
            
        }

        public async Task<BaseBookResult> InsertBookAsync(InsertBookRequest request)
        {
            var bookEntity = request.MapTo<BookEntity>();
            
            bookEntity.AuthorsBook = new List<AuthorBookEntity>();

            if (request.AuthorIds.IsNotNull() && request.AuthorIds.Any())
            {
                foreach (var authorId in request.AuthorIds)
                {
                    bookEntity.AuthorsBook.Add(new AuthorBookEntity
                    {
                        AuthorRef = authorId.Value
                    });
                }
            }

            await _bookRepository.AddAsync(bookEntity);
            
            try
            {
                await _db.SaveChangesAsync();

            }
            catch (Exception e)
            {
                throw;
            }

            return new BaseBookResult { Success = true };
        }

        public async Task<BaseBookResult> UpdateBookAsync(UpdateBookRequest request)
        {
            var updatedbookEntity = await _bookRepository.FirstOrDefaultAsync(x=>x.Key == request.Key);
            if (updatedbookEntity.IsNull())
            {
                throw new Exception("Book not found");
            }
            updatedbookEntity = request.MapTo<BookEntity>();
            
            
            _bookRepository.Update(updatedbookEntity);
            /*var authorEntity = await _authorRepository.FirstOrDefaultAsync(a =>   
                a.AuthorBooks.Any(ab => ab.BookRef == request.Key));
            
            if (authorEntity.IsNull())
            {
                throw new Exception("Not found");
            }
            
            authorEntity.AuthorName = request.AuthorName;
            
            
            _authorRepository.Update(authorEntity);*/
            
            try
            {
                await _db.SaveChangesAsync();

            }
            catch (Exception e)
            {
                throw;
            }

            return new BaseBookResult { Success = true };

        }

        public async Task<GetBooksResult> GetBooksAsync(GetBooksQuery query)
        {
            var finalResult = new GetBooksResult
            {
                Entities = new List<GetBooksModel>()
            };
            var queryDataSource = query.ToQueryDataSource<BookView>();

            
            //queryDataSource.AddFilter(new ExpressionFilterInfo<BookView>(x => x.UserRef == _appSession.UserRef));
            if (query.Key.HasValue)
                queryDataSource.AddFilter(new ExpressionFilterInfo<BookView>(x => x.Key == query.Key.Value));
            if(!string.IsNullOrEmpty(query.CategoryName))
                queryDataSource.AddFilter(new ExpressionFilterInfo<BookView>(x => x.CategoryName.Contains(query.CategoryName)));
            if(!string.IsNullOrEmpty(query.Title))
                queryDataSource.AddFilter(new ExpressionFilterInfo<BookView>(x => x.Title.Contains(query.Title)));
            if (!string.IsNullOrEmpty(query.AuthorName))
            {
                queryDataSource.AddFilter(new ExpressionFilterInfo<BookView>(x => x.AuthorName.Contains(query.AuthorName)));//x.AuthorNames.Any(author => author.Contains(query.AuthorName))));
                var result = await _bookRepository.GetBooksAsync(queryDataSource);
                if (result.HasAnyEntity())
                {
                    foreach (var book in result.Entities)
                    {
                        if (finalResult.Entities.Any(x => x.Key == book.Key))
                            continue;

                        var queryForBooksWithSameKey = new QueryDataSource<BookView>();
                        queryForBooksWithSameKey.AddFilter(new ExpressionFilterInfo<BookView>(x => x.Key == book.Key));

                        var allBooksWithSameKey = await _bookRepository.GetBooksAsync(queryForBooksWithSameKey);

                        var newModel = book.MapTo<GetBooksModel>();
                        newModel.AuthorNames = allBooksWithSameKey
                            .Entities 
                            .Select(x => x.AuthorName)
                            .ToList();
                        newModel.AuthorRefs = allBooksWithSameKey.Entities.Select(x=>x.AuthorRef.Value).Distinct().ToList();

                        finalResult.Entities.Add(newModel);
                    }
                }
            }
            else
            {
                var result = await _bookRepository.GetBooksAsync(queryDataSource);
                if (result.HasAnyEntity())
                {
                    foreach (var book in result.Entities)
                    {
                        if(finalResult.Entities.Where(x=>x.Key == book.Key).Any())
                            continue;
                        var books = result.Entities.Where(x => x.Key == book.Key);
                        var newModel = book.MapTo<GetBooksModel>();
                        if (book.AuthorName != null && book.AuthorName != string.Empty)
                        {
                            var authorNames = books.Select(x => x.AuthorName).ToList();
                            var authorRefs = books.Select(x => x.AuthorRef.Value).ToList();
                            newModel.AuthorNames = authorNames;
                            newModel.AuthorRefs = authorRefs;
                        }
                        
                        
 
                        finalResult.Entities.Add(newModel);
                    }
                } 
            }
            
            return finalResult;
        }

   

        public async Task<DeleteTypeIntResult> DeleteBookAsync(DeleteTypeIntRequest request)
        {
            await _bookRepository.DeleteWithAsync(x=>x.Key == request.Key);

            try
            {
                await _db.SaveChangesAsync();
            }
            catch (Exception e)
            {

                throw;
            }

            return new DeleteTypeIntResult { Success = true };
        }
        
        public async Task<AppendAuthorResult> AppendAuthorAsync(AppendAuthorRequest request)
        {
            var updatedbookautherEntity = await _authorBookRepository.FirstOrDefaultAsync(x=>x.BookRef == request.BookId);
            if (updatedbookautherEntity.IsNull())
            {
                foreach (var authorId in request.AuthorIds)
                {
                    var updatedauthorBookEntity = new AuthorBookEntity
                    {
                        BookRef = request.BookId,
                        AuthorRef = authorId,
                    };
                    await _authorBookRepository.AddAsync(updatedauthorBookEntity);
                }
            }
            else
            {
                await _authorBookRepository.DeleteWithAsync(x=>x.BookRef == request.BookId);
            

                foreach (var authorId in request.AuthorIds)
                {
                    var updatedauthorBookEntity = new AuthorBookEntity
                    {
                        BookRef = request.BookId,
                        AuthorRef = authorId,
                    };
                    await _authorBookRepository.AddAsync(updatedauthorBookEntity);
                }
            
                
            }
            try
            {
                await _db.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw;
            }
            
            
        
            return new AppendAuthorResult {Success = true};
        }

        public async Task<AppendCategoryResult> AppendCategoryAsync(AppendCategoryRequest request)
        {
            
            var categoryBookEntity = await _bookRepository.FirstOrDefaultAsync(x=>x.Key == request.BookId);
           
            categoryBookEntity.CategoryRef = request.CategoryId;
                
            _bookRepository.Update(categoryBookEntity);
            
            try
            {
                await _db.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw;
            }
            
            return new AppendCategoryResult {Success = true};
            
        }

        public async Task<GetByKeyBooksResult> GetByKeyBooksAsync(GetByKeyBooksQuery query)
        {
            var finalResult = new GetByKeyBooksResult
            {
                Entity = new GetByKeyBooksModel()
            };
            var queryDataSource = query.ToQueryDataSource<BookView>();
         
          
            
            queryDataSource.AddFilter(new ExpressionFilterInfo<BookView>(x => x.Key == query.Key));
           
            
            var result = await _bookRepository.GetBooksAsync(queryDataSource);
            
            if (result.HasAnyEntity())
            {
                    finalResult.Entity = result.Entities.First().MapTo<GetByKeyBooksModel>();
            } 
            
         
            return finalResult;
        }
    }
}
