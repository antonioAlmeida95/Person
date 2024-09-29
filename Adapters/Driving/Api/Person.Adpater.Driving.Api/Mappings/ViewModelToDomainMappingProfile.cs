using AutoMapper;
using Person.Adpater.Driving.Api.Dtos;
using Person.Core.Domain.Entities;

namespace Person.Adpater.Driving.Api.Mappings;

public class ViewModelToDomainMappingProfile : Profile
{
    public ViewModelToDomainMappingProfile()
    {
        CreateMap<PessoaViewModel, Pessoa>()
            .ForMember(s => s.Id, opt => opt.MapFrom(src => src.PessoaId))
            .AfterMap((src, des) =>
            {
                des.Id = src.PessoaId;
            });
    }
}