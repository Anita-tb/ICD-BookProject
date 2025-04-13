using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

[Dependency(typeof(IQuestionnarieService))]
public class QuestionnarieService : IQuestionnarieService
{
    private readonly IUnitOfWork _db;
    private readonly IQuestionnaireRepository _questionnaireRepository;

    public QuestionnarieService(IUnitOfWork db)
    {
        _db = db;
        _questionnaireRepository = _db.GetRepository<IQuestionnaireRepository>();
    }

    public async Task<GetQuestionnairesResult> GetQuestionnairesAsync(GetQuestionnairesQuery query)
    {
        var finalResult = new GetQuestionnairesResult
        {
            Entities = new List<GetQuestionnairesModel>()
        };

        
        var queryDataSource = query.ToQueryDataSource<QuestionnaireView>();

        
        if (query.Key.HasValue)
            queryDataSource.AddFilter(new ExpressionFilterInfo<QuestionnaireView>(x => x.Key == query.Key.Value));
        if(!string.IsNullOrEmpty(query.QuestionType))
            queryDataSource.AddFilter(new ExpressionFilterInfo<QuestionnaireView>(x => x.QuestionType.Contains(query.QuestionType)));

        
        var result = await _questionnaireRepository.GetQuestionnairesAsync(queryDataSource);

        
        var questionnaireDictionary = new Dictionary<int, GetQuestionnairesModel>();

        
        foreach (var item in result.Entities)
        {

            if (!questionnaireDictionary.ContainsKey(item.Key))
            {
                
                questionnaireDictionary[item.Key] = new GetQuestionnairesModel
                {
                    Key = item.Key,
                    Title = item.Title, 
                    Description = item.Description, 
                    Questions = new List<QuestionInfo>()
                };
            }

            var questionnaire = questionnaireDictionary[item.Key];

            if (item.QuestionKey.HasValue)
            {
                var question = questionnaire.Questions.FirstOrDefault(q => q.QuestionKey == item.QuestionKey);

                if (question == null)
                {
               
                    question = new QuestionInfo
                    {
                        QuestionKey = item.QuestionKey.Value,
                        QuestionText = item.QuestionText,
                        QuestionType = item.QuestionType
                    };

                
                    if (item.QuestionType == "type1")
                    {
                        question.Options = new List<OptionInfo>();
                    }

                    questionnaire.Questions.Add(question);
                }


                if (item.QuestionType == "type1" && item.OptionKey.HasValue)
                {
                    question.Options.Add(new OptionInfo
                    {
                        OptionKey = item.OptionKey.Value,
                        OptionText = item.OptionText
                    });
                    if (item.ResponseKey.HasValue)
                    {
                        question.ResponseInfo = new ResponseInfo
                        {
                            ResponseKey = item.ResponseKey.Value,
                            Answer = item.OptionRef.Value
                        };
                    }
                   
                }

                if (item.QuestionType == "type2" && item.ResponseKey.HasValue)
                {
                    question.ResponseInfo = new ResponseInfo
                    {
                        ResponseKey = item.ResponseKey.Value,
                        Answer = item.Digit.Value
                    };
                }
                if (item.QuestionType == "type3" && item.ResponseKey.HasValue)
                {
                    question.ResponseInfo = new ResponseInfo
                    {
                        ResponseKey = item.ResponseKey.Value,
                        Answer = item.Text
                    };
                }
                
            }
            
        }
        finalResult.Entities.AddRange(questionnaireDictionary.Values);
        
        return finalResult;
    }

    public async Task<BaseQuestionnaireResult> InsertQuestionnaireAsync(InsertQuestionnaireRequest request)
    {
        var questionnaireEntity = request.MapTo<QuestionnaireEntity>();
        await _questionnaireRepository.AddAsync(questionnaireEntity);
        try
        {
            await _db.SaveChangesAsync();

        }
        catch (Exception e)
        {
            throw;
        }

        return new BaseQuestionnaireResult{ Success = true };
    }

    public async Task<BaseQuestionnaireResult> UpdateQuestionnaireAsync(UpdateQuestionnaireRequest request)
    {
        var updatedquestionnaireEntity = await _questionnaireRepository.FirstOrDefaultAsync(x=>x.Key == request.Key);
        if (updatedquestionnaireEntity.IsNull())
        {
            throw new Exception("Questionnaire not found");
        }
        updatedquestionnaireEntity = request.MapTo<QuestionnaireEntity>();
            
            
        _questionnaireRepository.Update(updatedquestionnaireEntity);
        
        try
        {
            await _db.SaveChangesAsync();

        }
        catch (Exception e)
        {
            throw;
        }
        
        return new BaseQuestionnaireResult { Success = true };
    }

    public async Task<DeleteTypeIntResult> DeleteQuestionnaireAsync(DeleteTypeIntRequest request)
    {
        await _questionnaireRepository.DeleteWithAsync(x=>x.Key == request.Key);

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