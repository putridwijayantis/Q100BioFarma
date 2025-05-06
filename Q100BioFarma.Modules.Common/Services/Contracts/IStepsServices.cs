using Q100BioFarma.Infrastructur.Dtos;
using Q100BioFarma.Modules.Common.Dto.Requests;

namespace Q100BioFarma.Modules.Common.Services.Contracts;

/// <summary>
///     Steps Service Contracts
/// </summary>
public interface IStepsServices
{
    /// <summary>
    ///     Add Step
    /// </summary>
    Task<MessageDto> AddStep(Guid recipeId, StepRequest payload);
    
    /// <summary>
    ///     Add Sub Step
    /// </summary>
    Task<MessageDto> AddSubStep(Guid stepId, StepRequest payload);
    
    /// <summary>
    ///     Add Parameter
    /// </summary>
    Task<MessageDto> AddParameter(Guid stepId, ParameterRequest payload);
}