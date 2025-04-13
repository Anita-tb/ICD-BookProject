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


[Dependency(typeof(ICategoryService))]
public class CategoryService : ICategoryService
{
    private readonly IUnitOfWork _db;
    private readonly ICategoryRepository _categoryRepository;
    
    
    public CategoryService(IUnitOfWork db)
    {
        _db = db;
        _categoryRepository = _db.GetRepository<ICategoryRepository>();
    }
    
    
    public async Task<BaseCategoryResult> InsertCategoryAsync(InsertCategoryRequest request)
    {
        var categoryEntity = request.MapTo<CategoryEntity>();
        await _categoryRepository.AddAsync(categoryEntity);
        try
        {
            await _db.SaveChangesAsync();

        }
        catch (Exception e)
        {
            throw;
        }

        return new BaseCategoryResult{ Success = true };
    }

    public async Task<BaseCategoryResult> UpdateCategoryAsync(UpdateCategoryRequest request)
    {
        var updatedcategoryEntity = await _categoryRepository.FirstOrDefaultAsync(x=>x.Key == request.Key);
        if (updatedcategoryEntity.IsNull())
        {
            throw new Exception("Category not found");
        }
        updatedcategoryEntity = request.MapTo<CategoryEntity>();
            
            
        _categoryRepository.Update(updatedcategoryEntity);
        
        try
        {
            await _db.SaveChangesAsync();

        }
        catch (Exception e)
        {
            throw;
        }
        
        return new BaseCategoryResult{ Success = true };
    }

    public async Task<GetCategoriesResult> GetCategoriesAsync(GetCategoriesQuery query)
    {
        var queryDataSource = query.ToQueryDataSource<CategoryView>();
        
        if (query.Key.HasValue)
            queryDataSource.AddFilter(new ExpressionFilterInfo<CategoryView>(x => x.Key == query.Key.Value));
        if(!string.IsNullOrEmpty(query.Name))
            queryDataSource.AddFilter(new ExpressionFilterInfo<CategoryView>(x => x.Name.Contains(query.Name)));
        var result = await _categoryRepository.GetCategoriesAsync(queryDataSource);

        return result.MapTo<GetCategoriesResult>();
    }

    public async Task<DeleteTypeIntResult> DeleteCategoryAsync(DeleteTypeIntRequest request)
    {
        
        await _categoryRepository.DeleteWithAsync(x=>x.Key == request.Key);

        try
        {
            await _db.SaveChangesAsync();
        }
        catch (Exception e)
        {

            throw;         throw new Exception("Delete Category Failed", e);

        }

        return new DeleteTypeIntResult { Success = true };
    }
}