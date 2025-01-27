using ICD.Framework.Data.Repository;
using ICD.Framework.Model;
using ICD.Framework.QueryDataSource;
using ICD.BookProject.Domain.Entity;
using ICD.BookProject;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ICD.BookProject.Domain.View;

namespace ICD.BookProject.RepositoryContract
{
    public interface IBookRepository : IRepository<BookEntity, int>
    {
        Task<ListQueryResult<BookView>> GetBooksAsync(QueryDataSource<BookView> queryDataSource);
    }
}
