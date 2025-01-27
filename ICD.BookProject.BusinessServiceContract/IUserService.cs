using System.Threading.Tasks;
using ICD.BookProject;

namespace ICD.BookProject.BusinessServiceContract;

public interface IUserService
{
    Task<BaseUserResult> InsertUserAsync(InsertUserRequest request);
    Task<LoginUserResult> LoginUserAsync(LoginUserRequest request);
    Task<BaseUserResult> UpdateUserAsync(UpdateUserRequest request);
    Task<DeleteTypeLongResult> DeleteUserAsync(DeleteTypeLongRequest request);
    Task<GetUsersResult> GetUsersAsync(GetUsersQuery query);
    
}