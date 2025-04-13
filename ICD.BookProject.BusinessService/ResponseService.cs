using System;
using System.Threading.Tasks;
using Azure;
using ICD.BookProject.BusinessServiceContract;
using ICD.BookProject.Domain.Entity;
using ICD.BookProject.Domain.View;
using ICD.BookProject.RepositoryContract;
using ICD.Framework.Abstraction.Session;
using ICD.Framework.AppMapper.Extensions;
using ICD.Framework.Data.UnitOfWork;
using ICD.Framework.DataAnnotation;
using ICD.Framework.Extensions;
using ICD.Framework.QueryDataSource.Fitlter;

namespace ICD.BookProject.BusinessService;

[Dependency(typeof(IResponseService))]
public class ResponseService : IResponseService
{
    private readonly IUnitOfWork _db;
    private readonly IAppSession _appSession;
    private readonly IResponseRepository _responseRepository;
    private readonly IQuestionRepository _questionRepository;

    public ResponseService(IUnitOfWork db , IAppSession appSession)
    {
        _db = db;
        _appSession = appSession;
        _responseRepository = _db.GetRepository<IResponseRepository>();
        _questionRepository = _db.GetRepository<IQuestionRepository>();
    }

    public async Task<BaseResponseResult> InsertResponseAsync(InsertResponseRequest request)
    {
        var findresponseEntity = await _responseRepository.FirstOrDefaultAsync(x => x.QuestionRef == request.QuestionKey && x.UserRef == _appSession.UserRef);
        if (findresponseEntity != null)
        {
            await _responseRepository.DeleteWithAsync(x=>x.QuestionRef == request.QuestionKey && x.UserRef == _appSession.UserRef);
            
        }
        var questionEntity = await _questionRepository.FirstOrDefaultAsync(x => x.Key == request.QuestionKey);
        ResponseEntity responseEntity = null;

        if (questionEntity.Type == "type1")
        {
            try
            {
                responseEntity = new ResponseEntity
                {
                    UserRef = _appSession.UserRef,
                    QuestionRef = request.QuestionKey,
                    OptionRef = Convert.ToInt32(request.Answer)
                };
            }
            catch (Exception ex)
            {
                throw new Exception("Invalid answer format. Please enter an optionkey for type1.");
            }
        }
        else if (questionEntity.Type == "type2")
        {
            try
            {
                responseEntity = new ResponseEntity
                {
                    UserRef = _appSession.UserRef,
                    QuestionRef = request.QuestionKey,
                    Digit = Convert.ToInt32(request.Answer)
                };
            }
            catch (Exception ex)
            {
                throw new Exception("Invalid answer format. Please enter an integer for type2.");
            }
        }
        else if (questionEntity.Type == "type3")
        {
            responseEntity = new ResponseEntity
            {
                UserRef = _appSession.UserRef,
                QuestionRef = request.QuestionKey,
                Text = request.Answer.ToString() 
            };
        }
        else
        {
            throw new Exception("Invalid question type.");
        }
        await _responseRepository.AddAsync(responseEntity);
        try
        {
            await _db.SaveChangesAsync();

        }
        catch (Exception e)
        {
            throw;
        }

        return new BaseResponseResult { Success = true };
        
        
    }

    public async Task<GetResponsesResult> GetResponsesAsync(GetResponsesQuery query)
    {
        var queryDataSource = query.ToQueryDataSource<ResponseView>();
        
        queryDataSource.AddFilter(new ExpressionFilterInfo<ResponseView>(x => x.UserRef == _appSession.UserRef));
        if (query.Key.HasValue)
            queryDataSource.AddFilter(new ExpressionFilterInfo<ResponseView>(x => x.Key == query.Key.Value));
        if(!string.IsNullOrEmpty(query.QuestionType))
            queryDataSource.AddFilter(new ExpressionFilterInfo<ResponseView>(x => x.QuestionType.Contains(query.QuestionType)));
        if(query.QuestionKey.HasValue)
            queryDataSource.AddFilter(new ExpressionFilterInfo<ResponseView>(x => x.QuestionKey == query.QuestionKey));
        if(query.QuestionnaireKey.HasValue)
            queryDataSource.AddFilter(new ExpressionFilterInfo<ResponseView>(x => x.QuestionnaireRef == query.QuestionnaireKey.Value));
        
        
        
        
        var result = await _responseRepository.GetResponsesAsync(queryDataSource);

        return result.MapTo<GetResponsesResult>();
        
    }

    public async Task<DeleteTypeIntResult> DeleteResponseAsync(DeleteTypeIntRequest request)
    {
        await _responseRepository.DeleteWithAsync(x=>x.Key == request.Key);

        try
        {
            await _db.SaveChangesAsync();
        }
        catch (Exception e)
        {

            throw;
        }

        return new DeleteTypeIntResult { Success = true };
    }
}