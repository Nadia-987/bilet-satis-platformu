using EventTicket.API.Application.Interfaces;
using EventTicket.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace EventTicket.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SectionsController : ControllerBase
{
    private readonly ISectionService _sectionService;

    public SectionsController(ISectionService sectionService)
    {
        _sectionService = sectionService;
    }

    [HttpGet]
    public async Task<IActionResult> GetSections()
    {
        var sections = await _sectionService.GetSectionsAsync();

        return Ok(sections);
    }

    [HttpPost]
    public async Task<IActionResult> CreateSection(Section section)
    {
        var createdSection = await _sectionService.CreateSectionAsync(section);

        return Ok(createdSection);
    }
}