using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ICD.BookProject.BusinessServiceContract;
using ICD.BookProject.Domain.Entity;
using ICD.BookProject.Domain.View;
using ICD.BookProject.RepositoryContract;
using ICD.Framework.Abstraction.Session;
using ICD.Framework.AppMapper.Extensions;
using ICD.Framework.Data.UnitOfWork;
using ICD.Framework.DataAnnotation;
using ICD.Framework.Extensions;
using ICD.Framework.QueryDataSource;
using ICD.Framework.QueryDataSource.Fitlter;

namespace ICD.BookProject.BusinessService;

[Dependency(typeof(ILibraryService))]
public class LibraryService : ILibraryService
{
    private readonly IUnitOfWork _db;
    private readonly ILibraryRepository _libraryRepository;
    private readonly IAppSession _appSession;

    public LibraryService(IUnitOfWork db , IAppSession appSession)
    {
        _db = db;
        _libraryRepository = _db.GetRepository<ILibraryRepository>();
        _appSession = appSession;
    }

    public async Task<AddBookResult> AddBookAsync(AddBookRequest request)
    {
        var libraryEntity = new LibraryEntity
        {
            UserRef = _appSession.UserRef,
            BookRef = request.BookId,
            CurrentPage = request.CurrentPage.Value
        };
        await _libraryRepository.AddAsync(libraryEntity);
        try
        {
            await _db.SaveChangesAsync();

        }
        catch (Exception e)
        {
            throw;
        }

        return new AddBookResult { Success = true };
    }

    public async Task<UpdateCurrentPageResult> UpdateCurrentPageAsync(UpdateCurrentPageRequest request)
    {
        var updatedlibraryEntity = await _libraryRepository.FirstOrDefaultAsync(x=>x.Key == request.Key);
        if (updatedlibraryEntity.IsNull())
        {
            throw new Exception("Book not found");
        }
        updatedlibraryEntity.CurrentPage = request.CurrentPage;
            
            
        _libraryRepository.Update(updatedlibraryEntity);
        
        try
        {
            await _db.SaveChangesAsync();

        }
        catch (Exception e)
        {
            throw;
        }
        return new UpdateCurrentPageResult { Success = true };
        
    }

    public async Task<GetMyBooksResult> GetMyBooksAsync(GetMyBooksQuery query)
    {
        var finalResult = new GetMyBooksResult
            {
                Entities = new List<GetMyBooksModel>()
            };
            var queryDataSource = query.ToQueryDataSource<MyBookView>();

            
            queryDataSource.AddFilter(new ExpressionFilterInfo<MyBookView>(x => x.UserRef == 4));
            if (query.Key.HasValue)
                queryDataSource.AddFilter(new ExpressionFilterInfo<MyBookView>(x => x.Key == query.Key.Value));
            if(!string.IsNullOrEmpty(query.CategoryName))
                queryDataSource.AddFilter(new ExpressionFilterInfo<MyBookView>(x => x.CategoryName.Contains(query.CategoryName)));
            if(!string.IsNullOrEmpty(query.Title))
                queryDataSource.AddFilter(new ExpressionFilterInfo<MyBookView>(x => x.Title.Contains(query.Title)));
            if (!string.IsNullOrEmpty(query.AuthorName))
            {
                queryDataSource.AddFilter(new ExpressionFilterInfo<MyBookView>(x => x.AuthorName.Contains(query.AuthorName)));//x.AuthorNames.Any(author => author.Contains(query.AuthorName))));
                var result = await _libraryRepository.GetMyBooksAsync(queryDataSource);
                if (result.HasAnyEntity())
                {
                    foreach (var book in result.Entities)
                    {
                        if (finalResult.Entities.Any(x => x.BookRef == book.BookRef))
                            continue;

                        var queryForBooksWithSameKey = new QueryDataSource<MyBookView>();
                        queryForBooksWithSameKey.AddFilter(new ExpressionFilterInfo<MyBookView>(x => x.BookRef == book.BookRef));

                        var allBooksWithSameKey = await _libraryRepository.GetMyBooksAsync(queryForBooksWithSameKey);

                        var newModel = book.MapTo<GetMyBooksModel>();
                        newModel.AuthorNames = allBooksWithSameKey
                            .Entities 
                            .Select(x => x.AuthorName)
                            .ToList();

                        finalResult.Entities.Add(newModel);
                    }
                }
            }
            else
            {
                var result = await _libraryRepository.GetMyBooksAsync(queryDataSource);
                if (result.HasAnyEntity())
                {
                    foreach (var book in result.Entities)
                    {
                        if(finalResult.Entities.Where(x=>x.BookRef == book.BookRef).Any())
                            continue;
                       
                        var books = result.Entities.Where(x => x.BookRef == book.BookRef);
                        var newModel = book.MapTo<GetMyBooksModel>();
                        var authorNames = books.Select(x => x.AuthorName).ToList();
                        newModel.AuthorNames = authorNames;
 
                        finalResult.Entities.Add(newModel);
                    }
                } 
            }
            
            return finalResult;
    }

    public async Task<DeleteTypeIntResult> RemoveBookAsync(DeleteTypeIntRequest request)
    {
        await _libraryRepository.DeleteWithAsync(x=>x.Key == request.Key);

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
}