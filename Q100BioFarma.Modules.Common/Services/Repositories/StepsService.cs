using AutoMapper;
using Q100BioFarma.Database.Abstract.Contracts;
using Q100BioFarma.Infrastructur.Dtos;
using Q100BioFarma.Modules.Common.Dto.Requests;
using Q100BioFarma.Modules.Common.Models.Contracts;
using Q100BioFarma.Modules.Common.Models.Datas;
using Q100BioFarma.Modules.Common.Services.Contracts;

namespace Q100BioFarma.Modules.Common.Services.Repositories;

public class StepsService : IStepsServices
{
    private readonly IStorage _iStorage;
    private readonly IMapper _iMapper;

    public StepsService(IStorage iStorage, IMapper iMapper)
    {
        _iStorage = iStorage;
        _iMapper = iMapper;
    }
    
    public async Task<MessageDto> AddStep(Guid recipeId, StepRequest payload)
    {
        var newData = new Steps
        {
            RecipeId = recipeId,
            Name = payload.Name,
            Ordering = payload.Ordering
        };

        await _iStorage.GetRepository<IStepRepository>().AddOrUpdate(newData);
        await _iStorage.SaveAsync();
        
        return new MessageDto("Success", "Step added.");
    }

    public async Task<MessageDto> AddSubStep(Guid stepId, StepRequest payload)
    {
        var newData = new SubSteps
        {
            StepId = stepId,
            Name = payload.Name,
            Ordering = payload.Ordering
        };
        
        await _iStorage.GetRepository<ISubStepRepository>().AddOrUpdate(newData);
        await _iStorage.SaveAsync();
        
        return new MessageDto("Success", "Sub Step added.");
    }

    public async Task<MessageDto> AddParameter(Guid stepId, ParameterRequest payload)
    {
        var newData = new Parameters
        {
            StepId = stepId,
            Name = payload.Name,
            Type = payload.Type,
            Description = payload.Description
        };
        
        await _iStorage.GetRepository<IParameterRepository>().AddOrUpdate(newData);
        await _iStorage.SaveAsync();
        
        return new MessageDto("Success", "Parameter added.");
    }
}