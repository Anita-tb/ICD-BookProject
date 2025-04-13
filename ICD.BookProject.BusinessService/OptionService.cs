using System;
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

[Dependency(typeof(IOptionService))]
public class OptionService : IOptionService
{
    private readonly IUnitOfWork _db;
    private readonly IOptionRepository _optionRepository;
    private readonly IQuestionRepository _questionRepository;

    public OptionService(IUnitOfWork db)
    {
        _db = db;
        _optionRepository= _db.GetRepository<IOptionRepository>();
        _questionRepository = _db.GetRepository<IQuestionRepository>();
    }
    
    public async Task<BaseOptionResult> InsertOptionAsync(InsertOptionRequest request)
    {
        var checkquestionEntity = await _questionRepository.FirstOrDefaultAsync(x => x.Key == request.QuestionRef);
        if (checkquestionEntity == null)
        {
            throw new Exception("Question not found.");
        }

        if (checkquestionEntity.Type != "type1")
        {
            throw new Exception("Invalid question type. Expected type1.");
        }
        
        var optionEntity = request.MapTo<OptionEntity>();
        await _optionRepository.AddAsync(optionEntity);
        try
        {
            await _db.SaveChangesAsync();

        }
        catch (Exception e)
        {
            throw;
        }

        return new BaseOptionResult{ Success = true };
        
    }

    public async Task<BaseOptionResult> UpdateOptionAsync(UpdateOptionRequest request)
    {
        var updatedoptionEntity = await _optionRepository.FirstOrDefaultAsync(x=>x.Key == request.Key);
        if (updatedoptionEntity.IsNull())
        {
            throw new Exception("Option not found");
        }
        updatedoptionEntity = request.MapTo<OptionEntity>();
        
        var checkquestionEntity = await _questionRepository.FirstOrDefaultAsync(x => x.Key == request.QuestionRef);
        if (checkquestionEntity.Type != "type1")
        {
            throw new Exception("Invalid question type. Expected type1.");
        }
            
            
        _optionRepository.Update(updatedoptionEntity);
        
        try
        {
            await _db.SaveChangesAsync();

        }
        catch (Exception e)
        {
            throw;
        }
        
        return new BaseOptionResult{ Success = true };
    }

    public async Task<GetOptionsResult> GetOptionsAsync(GetOptionsQuery query)
    {
        var queryDataSource = query.ToQueryDataSource<Optionview>();
        
        if (query.Key.HasValue)
            queryDataSource.AddFilter(new ExpressionFilterInfo<Optionview>(x => x.Key == query.Key.Value));
        if (query.QuestionRef.HasValue)
            queryDataSource.AddFilter(new ExpressionFilterInfo<Optionview>(x => x.QuestionRef == query.QuestionRef.Value));
       
        
        var result = await _optionRepository.GetOptionsAsync(queryDataSource);

        return result.MapTo<GetOptionsResult>();
    }
    
    public async Task<DeleteTypeIntResult> DeleteOptionAsync(DeleteTypeIntRequest request)
    {
        await _optionRepository.DeleteWithAsync(x=>x.Key == request.Key);

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