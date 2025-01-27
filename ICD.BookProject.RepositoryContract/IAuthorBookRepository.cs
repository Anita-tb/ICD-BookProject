using ICD.BookProject.Domain.Entity;
using ICD.Framework.Data.Repository;

namespace ICD.BookProject.RepositoryContract;

public interface IAuthorBookRepository : IRepository<AuthorBookEntity,int>
{
    
}