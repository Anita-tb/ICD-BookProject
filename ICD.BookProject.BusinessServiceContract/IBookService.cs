using ICD.BookProject;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ICD.BookProject;

namespace ICD.BookProject.BusinessServiceContract
{
    public interface IBookService
    {
        Task<BaseBookResult> InsertBookAsync(InsertBookRequest request);
        Task<BaseBookResult> UpdateBookAsync(UpdateBookRequest request);
        Task<GetBooksResult> GetBooksAsync(GetBooksQuery query);
        Task<DeleteTypeIntResult> DeleteBookAsync(DeleteTypeIntRequest request);
        Task<AppendAuthorResult> AppendAuthorAsync(AppendAuthorRequest request);
        Task<AppendCategoryResult> AppendCategoryAsync(AppendCategoryRequest request);
    }
    
    
    
}
