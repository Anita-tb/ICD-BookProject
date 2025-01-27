using ICD.Framework.AppMapper;
using ICD.Framework.Model;
using ICD.BookProject.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using ICD.BookProject.Domain.View;

namespace ICD.BookProject.BusinessService.Mapping
{
    public class MapInitializer : IMapperInitializer
    {
        public void Initialize(IMapperConfiguration config)
        {
            #region Book
            config.CreateMap<InsertBookRequest, BookEntity>();
            config.CreateMap<UpdateBookRequest, BookEntity>();
            
            config.CreateMap<BookView, GetBooksModel>();
            config.CreateMap<ListQueryResult<BookView>, GetBooksResult>();
            #endregion
            
            #region Author
            config.CreateMap<InsertAuthorRequest, AuthorEntity>();
            config.CreateMap<UpdateAuthorRequest, AuthorEntity>();
            
            config.CreateMap<AuthorView, GetAuthorsModel>();
            config.CreateMap<ListQueryResult<AuthorView>, GetAuthorsResult>();
            #endregion
            
            #region User
            config.CreateMap<InsertUserRequest, UserEntity>();
            config.CreateMap<UpdateUserRequest, UserEntity>();
            
            config.CreateMap<UserView, GetUsersModel>();
            config.CreateMap<ListQueryResult<UserView>, GetUsersResult>();
            #endregion

            #region Category

            config.CreateMap<InsertCategoryRequest, CategoryEntity>();
            config.CreateMap<UpdateCategoryRequest, CategoryEntity>();
            
            config.CreateMap<CategoryView, GetCategoriesModel>();
            config.CreateMap<ListQueryResult<CategoryView>, GetCategoriesResult>();

            #endregion

            #region Library

            //config.CreateMap<AddBookRequest, LibraryEntity>();
            config.CreateMap<UpdateCurrentPageRequest, LibraryEntity>();
            
            config.CreateMap<MyBookView, GetMyBooksModel>();
            config.CreateMap<ListQueryResult<MyBookView>, GetMyBooksResult>();

            #endregion
            
        }
    }
}
