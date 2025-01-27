using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ICD.BookProject.Domain.Entity;
using ICD.BookProject.Domain.View;
using ICD.Framework.Data.Repository;
using ICD.Framework.Model;
using ICD.Framework.QueryDataSource;

namespace ICD.BookProject.RepositoryContract;

public interface IAuthorRepository : IRepository<AuthorEntity,int>
{
    Task<ListQueryResult<AuthorView>> GetAuthorsAsync(QueryDataSource<AuthorView> queryDataSource);
}