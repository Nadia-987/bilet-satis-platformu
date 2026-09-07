using EventTicket.Domain.Entities;
using EventTicket.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EventTicket.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SectionsController : ControllerBase
{
    private readonly AppDbContext _context;

    public SectionsController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetSections()
    {
        var sections = await _context.Sections.ToListAsync();

        return Ok(sections);
    }

    [HttpPost]
    public async Task<IActionResult> CreateSection(Section section)
    {
        _context.Sections.Add(section);
        await _context.SaveChangesAsync();

        return Ok(section);
    }
}
