using ICD.BookProject.Domain.Entity;
using ICD.BookProject.RepositoryContract;
using ICD.Framework.Data.Repository;
using ICD.Framework.Data.UnitOfWork;
using ICD.Framework.DataAnnotation;

namespace ICD.BookProject.Repository;

[Dependency(typeof(IAuthorBookRepository))]
public class AuthorBookRepository : BaseRepository<AuthorBookEntity,int> , IAuthorBookRepository
{
    
}