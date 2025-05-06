using Microsoft.AspNetCore.Mvc;
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
}