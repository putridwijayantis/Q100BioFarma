using Q100BioFarma.Modules.Common.Models.Datas;

namespace Q100BioFarma.Modules.Common.Dto.Responses;

public class StepsResponse
{
    public Guid Id { get; set; }
    
    public string Name { get; set; }

    public int Ordering { get; set; }
    
    public List<SubStepsResponse> SubSteps { get; set; }
    
    public List<ParameterResponse> Parameters { get; set; }
}