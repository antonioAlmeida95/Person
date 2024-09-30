using AutoMapper;
using Person.Adpater.Driving.Api.Dtos;
using Person.Core.Domain.Entities;

namespace Person.Adpater.Driving.Api.Mappings;

public class DomainToViewModelMappingProfile : Profile
{
    public DomainToViewModelMappingProfile()
    {
        CreateMap<Pessoa, PessoaViewModel>()
            .ForMember(s=> s.PessoaId, opt => opt.MapFrom(src => src.Id));
    }
}