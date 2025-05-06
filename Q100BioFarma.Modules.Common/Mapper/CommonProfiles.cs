using AutoMapper;
using Q100BioFarma.Modules.Common.Dto.Responses;
using Q100BioFarma.Modules.Common.Models.Datas;

namespace Q100BioFarma.Modules.Common.Mapper;

public class CommonProfiles : Profile
{
    public CommonProfiles()
    {
        CreateMap<Recipes, RecipesResponse>().ReverseMap();

        CreateMap<Steps, StepsResponse>().ReverseMap();
        
        CreateMap<SubSteps, SubStepsResponse>().ReverseMap();
    }
}