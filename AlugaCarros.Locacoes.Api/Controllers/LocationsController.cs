using AlugaCarros.Locacoes.Domain.Interfaces;
using AlugaCarros.Locacoes.Domain.RequestResponse;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AlugaCarros.Locacoes.Api.Controllers;
[Route("api/v1/[controller]")]
[ApiController]
[Authorize]
public class LocationsController : ControllerBase
{
    private readonly ILocationService _locationService;

    public LocationsController(ILocationService locationService)
    {
        _locationService = locationService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateLocation(CreateLocationRequest request)
    {
        var createLocationResponse = await _locationService.CreateLocation(request);

        if (createLocationResponse.Fail) return BadRequest(createLocationResponse.Message);

        return Ok(new { LocationCode = createLocationResponse.Data });
    }

    [HttpGet("{agencyCode}")]
    public async Task<IActionResult> GetLocations([FromRoute] string agencyCode)
    {
        var locations = await _locationService.GetLocations(agencyCode);

        return Ok(locations);
    }
}
