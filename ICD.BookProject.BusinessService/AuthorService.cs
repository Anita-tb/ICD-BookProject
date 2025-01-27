using System;
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
using ICD.Framework.QueryDataSource.Fitlter;

namespace ICD.BookProject.BusinessService;

[Dependency(typeof(IAuthorService))]
public class AuthorService : IAuthorService
{
    private readonly IUnitOfWork _db;
    private readonly IAuthorRepository _authorRepository;
    
    public AuthorService(IUnitOfWork db)
    {
        _db = db;
        _authorRepository = _db.GetRepository<IAuthorRepository>();
    }

    public async Task<BaseAuthorResult> InsertAuthorAsync(InsertAuthorRequest request)
    {
        var authorEntity = request.MapTo<AuthorEntity>();
        await _authorRepository.AddAsync(authorEntity);
        try
        {
            await _db.SaveChangesAsync();

        }
        catch (Exception e)
        {
            throw;
        }

        return new BaseAuthorResult{ Success = true };

    }
    
    public async Task<BaseAuthorResult> UpdateAuthorAsync(UpdateAuthorRequest request)
    {
        var updatedauthorEntity = await _authorRepository.FirstOrDefaultAsync(x=>x.Key == request.Key);
        if (updatedauthorEntity.IsNull())
        {
            throw new Exception("Author not found");
        }
        updatedauthorEntity = request.MapTo<AuthorEntity>();
            
            
        _authorRepository.Update(updatedauthorEntity);
        try
        {
            await _db.SaveChangesAsync();

        }
        catch (Exception e)
        {
            throw;
        }
        
        return new BaseAuthorResult{ Success = true };

    }

    public async Task<GetAuthorsResult> GetAuthorsAsync(GetAuthorsQuery query)
    {
        var queryDataSource = query.ToQueryDataSource<AuthorView>();
        
        //queryDataSource.AddFilter(new ExpressionFilterInfo<AuthorView>(x => x.UserRef == _appSession.UserRef));
        if (query.Key.HasValue)
            queryDataSource.AddFilter(new ExpressionFilterInfo<AuthorView>(x => x.Key == query.Key.Value));
        if(!string.IsNullOrEmpty(query.Name))
            queryDataSource.AddFilter(new ExpressionFilterInfo<AuthorView>(x => x.Name.Contains(query.Name)));

        var result = await _authorRepository.GetAuthorsAsync(queryDataSource);

        return result.MapTo<GetAuthorsResult>();
    }

    public async Task<DeleteTypeIntResult> DeleteAuthorAsync(DeleteTypeIntRequest request)
    {
        await _authorRepository.DeleteWithAsync(x=>x.Key == request.Key);

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