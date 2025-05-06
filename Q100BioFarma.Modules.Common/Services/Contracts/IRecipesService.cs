using Q100BioFarma.Infrastructur.Dtos;
using Q100BioFarma.Modules.Common.Dto.Requests;
using Q100BioFarma.Modules.Common.Dto.Responses;
using Q100BioFarma.Modules.Common.Models.Datas;

namespace Q100BioFarma.Modules.Common.Services.Contracts;

/// <summary>
///     Recipes Service Contracts
/// </summary>
public interface IRecipesService
{
    /// <summary>
    ///     Get All
    /// </summary>
    Task<List<RecipeListResponse>> GetAll();
    
    /// <summary>
    ///     Get By Id
    /// </summary>
    Task<RecipesResponse> GetById(Guid id);
    
    /// <summary>
    ///     Create
    /// </summary>
    Task<MessageDto> Create(RecipesRequest payload);
    
    /// <summary>
    ///     Update
    /// </summary>
    Task<MessageDto> Update(Guid id, RecipesRequest payload);
    
    /// <summary>
    ///     Delete
    /// </summary>
    Task<MessageDto> Delete(Guid id);
}