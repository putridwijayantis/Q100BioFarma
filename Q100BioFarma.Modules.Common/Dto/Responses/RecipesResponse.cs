namespace Q100BioFarma.Modules.Common.Dto.Responses;

public class RecipesResponse
{
    public Guid Id { get; set; }
    
    public string Name { get; set; }
    
    public string Description { get; set; }
    
    public List<StepsResponse> Steps { get; set; }
}