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
            #region Author
            config.CreateMap<InsertAuthorRequest, AuthorEntity>();
            config.CreateMap<UpdateAuthorRequest, AuthorEntity>();
            
            config.CreateMap<AuthorView, GetAuthorsModel>();
            config.CreateMap<ListQueryResult<AuthorView>, GetAuthorsResult>();
            #endregion
            
            #region Book
            config.CreateMap<InsertBookRequest, BookEntity>();
            config.CreateMap<UpdateBookRequest, BookEntity>();
            
            config.CreateMap<BookView, GetBooksModel>();
            config.CreateMap<BookView, GetByKeyBooksModel>();
            config.CreateMap<ListQueryResult<BookView>, GetBooksResult>();
            config.CreateMap<SingleQueryResult<BookView>, GetByKeyBooksResult>();
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

            #region Option

            config.CreateMap<InsertOptionRequest, OptionEntity>();
            config.CreateMap<UpdateOptionRequest, OptionEntity>();
            
            config.CreateMap<Optionview, GetOptionsModel>();
            config.CreateMap<ListQueryResult<Optionview>, GetOptionsResult>();

            #endregion
            
            #region Question

            config.CreateMap<InsertQuestionRequest, QuestionEntity>();
            config.CreateMap<UpdateQuestionRequest, QuestionEntity>();

            #endregion

            #region Questionnarie
            
            config.CreateMap<InsertQuestionnaireRequest, QuestionnaireEntity>();
            config.CreateMap<UpdateQuestionnaireRequest, QuestionnaireEntity>();
            
            /*config.CreateMap<QuestionnaireView, GetQuestionnairesModel>();
            config.CreateMap<ListQueryResult<QuestionnaireView>, GetQuestionnairesResult>();*/

            #endregion

            #region Response

            config.CreateMap<ResponseView, GetResponsesModel>();
            config.CreateMap<ListQueryResult<ResponseView>, GetResponsesResult>();

            #endregion
            
            #region User
            config.CreateMap<InsertUserRequest, UserEntity>();
            config.CreateMap<UpdateUserRequest, UserEntity>();
            
            config.CreateMap<UserView, GetUsersModel>();
            config.CreateMap<ListQueryResult<UserView>, GetUsersResult>();
            #endregion
            
        }
    }
}
