using System.Threading.Tasks;

namespace ICD.BookProject.BusinessServiceContract;

public interface ICategoryService
{
    Task<BaseCategoryResult> InsertCategoryAsync(InsertCategoryRequest request);
    Task<BaseCategoryResult> UpdateCategoryAsync(UpdateCategoryRequest request);
    Task<GetCategoriesResult> GetCategoriesAsync(GetCategoriesQuery query);
    Task<DeleteTypeIntResult> DeleteCategoryAsync(DeleteTypeIntRequest request);
}