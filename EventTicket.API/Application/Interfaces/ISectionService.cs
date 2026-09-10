using EventTicket.Domain.Entities;

namespace EventTicket.API.Application.Interfaces;

public interface ISectionService
{
    Task<IEnumerable<Section>> GetSectionsAsync();
    Task<Section> CreateSectionAsync(Section section);
}
