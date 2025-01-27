using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using ICD.BookProject.BusinessServiceContract;
using ICD.BookProject.Domain.Entity;
using ICD.BookProject.RepositoryContract;
using ICD.BookProject;
using ICD.BookProject.Domain.View;
using ICD.Framework.Abstraction.Session;
using ICD.Framework.AppMapper.Extensions;
using ICD.Framework.Data.UnitOfWork;
using ICD.Framework.DataAnnotation;
using ICD.Framework.Extensions;
using ICD.Framework.QueryDataSource.Fitlter;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;

namespace ICD.BookProject.BusinessService;

[Dependency(typeof(IUserService))]
public class UserService : IUserService
{
    private readonly IUnitOfWork _db;
    private readonly IUserRepository _userRepository;
    
    

    public UserService(IUnitOfWork db)
    {
        _db = db;   
        _userRepository = _db.GetRepository<IUserRepository>();
        
    }

    public async Task<BaseUserResult> InsertUserAsync(InsertUserRequest request)
    {
       var userEntity = request.MapTo<UserEntity>();
       await _userRepository.AddAsync(userEntity);
       try
       {
           await _db.SaveChangesAsync();

       }
       catch (Exception e)
       {
           throw;
       }

       return new BaseUserResult{ Success = true };
    }
    public async Task<LoginUserResult> LoginUserAsync(LoginUserRequest request)
    {
        var lgEntity = await _userRepository.FirstOrDefaultAsync(u => u.Username == request.Username && u.Password == request.Password);
        string token = string.Empty;
        
        if (lgEntity != null)
        {
            token = await CreateToken(lgEntity.Key, new CreateTokenQuery()
            {
                Username = lgEntity.Username
                
            });
        }

        return new LoginUserResult { Success = true, Entity = new LoginUserModel { Token = token } };
    }
    public async Task<BaseUserResult> UpdateUserAsync(UpdateUserRequest request)
    {
        var updateduserEntity = await _userRepository.FirstOrDefaultAsync(x=>x.Key == request.Key);
        if (updateduserEntity.IsNull())
        {
            throw new Exception("User not found");
        }
        
        updateduserEntity = request.MapTo<UserEntity>();
        _userRepository.Update(updateduserEntity);
        try
        {
            await _db.SaveChangesAsync();

        }
        catch (Exception e)
        {
            throw;
        }
        return  new BaseUserResult { Success = true };
    }

    public async Task<DeleteTypeLongResult> DeleteUserAsync(DeleteTypeLongRequest request)
    {
        await _userRepository.DeleteWithAsync(x=>x.Key == request.Key);

        try
        {
            await _db.SaveChangesAsync();
        }
        catch (Exception e)
        {

            throw;
        }

        return new DeleteTypeLongResult { Success = true };
    }

    public async Task<GetUsersResult> GetUsersAsync(GetUsersQuery query)
    {
        var queryDataSource = query.ToQueryDataSource<UserView>();
        
        if (query.Key.HasValue)
            queryDataSource.AddFilter(new ExpressionFilterInfo<UserView>(x => x.Key == query.Key.Value));
        if(!string.IsNullOrEmpty(query.Username))
            queryDataSource.AddFilter(new ExpressionFilterInfo<UserView>(x => x.Username.Contains(query.Username)));
        if(!string.IsNullOrEmpty(query.Password))
            queryDataSource.AddFilter(new ExpressionFilterInfo<UserView>(x => x.Password.Contains(query.Password)));
        
        var result = await _userRepository.GetUsersAsync(queryDataSource);

        return result.MapTo<GetUsersResult>();
    }

    private async Task<string> CreateToken(long userKey, CreateTokenQuery query)
    {

        DateTime issuedAt = DateTime.UtcNow;

        DateTime expires = DateTime.UtcNow.AddHours(query.ExpireAfterHours);
 
        var tokenHandler = new JwtSecurityTokenHandler();
 
        ClaimsIdentity claimsIdentity = new ClaimsIdentity(new[] {

            new Claim ("UserKey", userKey.ToString ()),
            new Claim ("UserName", query.Username)

        });
        
 
        var securityKey = new SymmetricSecurityKey(Encoding.Default.GetBytes(query.SecretKey));
 
        var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);
 
        var token =

            tokenHandler.CreateJwtSecurityToken(issuer: query.Issuer,

                subject: claimsIdentity, notBefore: issuedAt, expires: expires, signingCredentials: signingCredentials);
 
        var tokenString = tokenHandler.WriteToken(token);
 
        return tokenString;
 
    }

}