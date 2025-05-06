using AutoMapper;
using Q100BioFarma.Database.Abstract.Contracts;
using Q100BioFarma.Infrastructur.Constants;
using Q100BioFarma.Infrastructur.Dtos;
using Q100BioFarma.Modules.Common.Dto.Requests;
using Q100BioFarma.Modules.Common.Dto.Responses;
using Q100BioFarma.Modules.Common.Models.Contracts;
using Q100BioFarma.Modules.Common.Models.Datas;
using Q100BioFarma.Modules.Common.Services.Contracts;

namespace Q100BioFarma.Modules.Common.Services.Repositories;

public class RecipesService : IRecipesService
{
    
    private readonly IStorage _iStorage;
    private readonly IMapper _iMapper;

    public RecipesService(IStorage iStorage, IMapper iMapper)
    {
        _iStorage = iStorage;
        _iMapper = iMapper;
    }
    
    public async Task<List<RecipeListResponse>> GetAll()
    {
        var data = await _iStorage.GetRepository<IRecipesRepository>().GetAllData();
        return _iMapper.Map<List<RecipeListResponse>>(data);
    }

    public async Task<RecipesResponse> GetById(Guid id)
    {
        var data = await _iStorage.GetRepository<IRecipesRepository>().GetDetail(id);
        if (data == null) throw Error.CustomErrorBadRequest("The given id does not exist.");
        return _iMapper.Map<RecipesResponse>(data);
    }

    public async Task<MessageDto> Create(RecipesRequest payload)
    {
        var newData = new Recipes()
        {
            Name = payload.Name,
            Description = payload.Description,
        };
        
        await _iStorage.GetRepository<IRecipesRepository>().AddOrUpdate(newData);
        await _iStorage.SaveAsync();
        
        return new MessageDto("Success", "Recipes created.");
    }

    public async Task<MessageDto> Update(Guid id, RecipesRequest payload)
    {
        var data = await _iStorage.GetRepository<IRecipesRepository>().GetById(id);
        if (data == null)
        {
            throw Error.CustomErrorBadRequest("The given id does not exist.");
        }
        
        data.Name = payload.Name;
        data.Description = payload.Description;
        
        await _iStorage.GetRepository<IRecipesRepository>().AddOrUpdate(data);
        await _iStorage.SaveAsync();
        
        return new MessageDto("Success", "Recipes updated.");
    }

    public async Task<MessageDto> Delete(Guid id)
    {
        var data = await _iStorage.GetRepository<IRecipesRepository>().GetById(id);
        if (data == null)
        {
            throw Error.CustomErrorBadRequest("The given id does not exist.");
        }
        
        await _iStorage.GetRepository<IRecipesRepository>().Delete(data);
        await _iStorage.SaveAsync();
        
        return new MessageDto("Success", "Recipes deleted.");
    }
}