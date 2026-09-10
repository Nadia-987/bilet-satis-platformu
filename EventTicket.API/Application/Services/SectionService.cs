using EventTicket.API.Application.Interfaces;
using EventTicket.Domain.Entities;
using EventTicket.Infrastructure.Repositories;

namespace EventTicket.API.Application.Services;

public class SectionService : ISectionService
{
    private readonly ISectionRepository _sectionRepository;

    public SectionService(ISectionRepository sectionRepository)
    {
        _sectionRepository = sectionRepository;
    }

    public async Task<IEnumerable<Section>> GetSectionsAsync()
    {
        return await _sectionRepository.GetAllAsync();
    }

    public async Task<Section> CreateSectionAsync(Section section)
    {
        await _sectionRepository.AddAsync(section);
        await _sectionRepository.SaveAsync();

        return section;
    }
}