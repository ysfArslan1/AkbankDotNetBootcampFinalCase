using AutoMapper;
using FinalCase.Data.Entity;
using FinalCase.Schema;

namespace FinalCase.Business.Mapper;

public class MapperConfig : Profile
{
    public MapperConfig()
    {

        CreateMap<ContactRequest, Contact>();
        CreateMap<Contact, ContactResponse>()
            .ForMember(dest => dest.UserName,
                src => src.MapFrom(x => x.User.FirstName + " " + x.User.LastName));

       
    }
}