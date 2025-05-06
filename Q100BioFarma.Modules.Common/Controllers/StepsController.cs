using Microsoft.AspNetCore.Mvc;
using Q100BioFarma.Infrastructur.Dtos;
using Q100BioFarma.Infrastructur.Exceptions;
using Q100BioFarma.Modules.Common.Dto.Requests;
using Q100BioFarma.Modules.Common.Services.Contracts;

namespace Q100BioFarma.Modules.Common.Controllers;

[ApiController]
[Produces("application/json")]
[Route("api/steps")]
public class StepsController : Controller
{
    private readonly IStepsServices _stepsServices;

    public StepsController(IStepsServices stepsServices)
    {
        _stepsServices = stepsServices;
    }
    
    [HttpPost("{recipeId}")]
    public async Task<IActionResult> AddStep(Guid recipeId, [FromBody] StepRequest payload)
    {
        try
        {
            var data = await _stepsServices.AddStep(recipeId, payload);
            return Ok(data);
        }
        catch (HttpResponseLibraryException ex)
        {
            return StatusCode(ex.Code, new ResponseDto(ex.Code, new MessageDto(ex.Title, ex.Message), null));
        }
        
    }
    
    [HttpPost("{stepId}/sub-step")]
    public async Task<IActionResult> AddSubStep(Guid stepId, [FromBody] StepRequest payload)
    {
        try
        {
            var data = await _stepsServices.AddSubStep(stepId, payload);
            return Ok(data);
        }
        catch (HttpResponseLibraryException ex)
        {
            return StatusCode(ex.Code, new ResponseDto(ex.Code, new MessageDto(ex.Title, ex.Message), null));
        }
        
    }
    
    [HttpPost("{stepId}/parameter")]
    public async Task<IActionResult> AddParameter(Guid stepId, [FromBody] ParameterRequest payload)
    {
        try
        {
            var data = await _stepsServices.AddParameter(stepId, payload);
            return Ok(data);
        }
        catch (HttpResponseLibraryException ex)
        {
            return StatusCode(ex.Code, new ResponseDto(ex.Code, new MessageDto(ex.Title, ex.Message), null));
        }
        
    }
}