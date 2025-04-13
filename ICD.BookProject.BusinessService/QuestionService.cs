using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ICD.BookProject.BusinessServiceContract;
using ICD.BookProject.Domain.Entity;
using ICD.BookProject.Domain.View;
using ICD.BookProject.RepositoryContract;
using ICD.Framework.AppMapper.Extensions;
using ICD.Framework.Data.UnitOfWork;
using ICD.Framework.DataAnnotation;
using ICD.Framework.Extensions;
using ICD.Framework.QueryDataSource.Fitlter;

namespace ICD.BookProject.BusinessService;


[Dependency(typeof(IQuestionService))]
public class QuestionService  : IQuestionService
{
    private readonly IUnitOfWork _db;
    private readonly IQuestionRepository _questionRepository;

    public QuestionService(IUnitOfWork db)
    {
        _db = db;
        _questionRepository = _db.GetRepository<IQuestionRepository>();
    }

    public async Task<BaseQuestionResult> InsertQuestionAsync(InsertQuestionRequest request)
    {
        var questionEntity = request.MapTo<QuestionEntity>();
        await _questionRepository.AddAsync(questionEntity);
        
        try
        {
            await _db.SaveChangesAsync();

        }
        catch (Exception e)
        {
            throw;
        }

        return new BaseQuestionResult { Success = true };
        
    }

    public async Task<GetQuestionsResult> GetQuestionsAsync(GetQuestionsQuery query)
    {
        var finalResult = new GetQuestionsResult
        {
            Entities = new List<GetQuestionsModel>()
        };

        var queryDataSource = query.ToQueryDataSource<QuestionView>();

        if (query.Key.HasValue)
            queryDataSource.AddFilter(new ExpressionFilterInfo<QuestionView>(x => x.Key == query.Key.Value));
        if (!string.IsNullOrEmpty(query.Type))
            queryDataSource.AddFilter(new ExpressionFilterInfo<QuestionView>(x => x.Type.Contains(query.Type)));

        var result = await _questionRepository.GetQuestionsAsync(queryDataSource);

        var questionDictionary = new Dictionary<int, GetQuestionsModel>();

        foreach (var item in result.Entities)
        {
            if (!questionDictionary.ContainsKey(item.Key))
            {
                questionDictionary[item.Key] = new GetQuestionsModel
                {
                    Key = item.Key,
                    Text = item.Text,
                    Type = item.Type,
                    QuestionnaireRef = item.QuestionnaireRef,
                    QuestionnaireTitle = item.QuestionnaireTitle,
                    Options = new List<OptionInfo>()
                };
            }

           
            if (item.OptionKey.HasValue)
            {
                questionDictionary[item.Key].Options.Add(new OptionInfo
                {
                    OptionKey = item.OptionKey.Value,
                    OptionText = item.OptionText
                });
            }
        }

        finalResult.Entities.AddRange(questionDictionary.Values);

        return finalResult;

        
    }

    public async Task<BaseQuestionResult> UpdateQuestionAsync(UpdateQuestionRequest request)
    {
        var updatedquestionEntity = await _questionRepository.FirstOrDefaultAsync(x=>x.Key == request.Key);
        if (updatedquestionEntity.IsNull())
        {
            throw new Exception("Questionnaire not found");
        }
        updatedquestionEntity = request.MapTo<QuestionEntity>();
            
            
        _questionRepository.Update(updatedquestionEntity);
        
        try
        {
            await _db.SaveChangesAsync();

        }
        catch (Exception e)
        {
            throw;
        }
        
        return new BaseQuestionResult { Success = true };
    }

    public async Task<DeleteTypeIntResult> DeleteQuestionAsync(DeleteTypeIntRequest request)
    {
        await _questionRepository.DeleteWithAsync(x=>x.Key == request.Key);

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