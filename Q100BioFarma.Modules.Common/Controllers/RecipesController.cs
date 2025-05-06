using Microsoft.AspNetCore.Mvc;
using Q100BioFarma.Infrastructur.Dtos;
using Q100BioFarma.Infrastructur.Exceptions;
using Q100BioFarma.Modules.Common.Dto.Requests;
using Q100BioFarma.Modules.Common.Services.Contracts;

namespace Q100BioFarma.Modules.Common.Controllers;

[ApiController]
[Produces("application/json")]
[Route("api/recipes")]
public class RecipesController : Controller
{
    private readonly IRecipesService _recipesService;

    public RecipesController(IRecipesService recipesService)
    {
        _recipesService = recipesService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var data = await _recipesService.GetAll();
        return Ok(data);
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        try
        {
            var data = await _recipesService.GetById(id);
            return Ok(data);
        }
        catch (HttpResponseLibraryException ex)
        {
            return StatusCode(ex.Code, new ResponseDto(ex.Code, new MessageDto(ex.Title, ex.Message), null));
        }
        
    }
    
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] RecipesRequest payload)
    {
        try
        {
            var data = await _recipesService.Create(payload);
            return Ok(data);
        }
        catch (HttpResponseLibraryException ex)
        {
            return StatusCode(ex.Code, new ResponseDto(ex.Code, new MessageDto(ex.Title, ex.Message), null));
        }
        
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> Update([FromBody] RecipesRequest payload, Guid id)
    {
        try
        {
            var data = await _recipesService.Update(id, payload);
            return Ok(data);
        }
        catch (HttpResponseLibraryException ex)
        {
            return StatusCode(ex.Code, new ResponseDto(ex.Code, new MessageDto(ex.Title, ex.Message), null));
        }
        
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        try
        {
            var data = await _recipesService.Delete(id);
            return Ok(data);
        }
        catch (HttpResponseLibraryException ex)
        {
            return StatusCode(ex.Code, new ResponseDto(ex.Code, new MessageDto(ex.Title, ex.Message), null));
        }
        
    }
}