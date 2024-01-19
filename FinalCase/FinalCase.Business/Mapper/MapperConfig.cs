using AutoMapper;
using FinalCase.Data.Entity;
using FinalCase.Schema;

namespace FinalCase.Business.Mapper;

public class MapperConfig : Profile
{
    public MapperConfig()
    {

        CreateMap<CreateContactRequest, Contact>();
        CreateMap<Contact, ContactResponse>()
            .ForMember(dest => dest.UserName,
                src => src.MapFrom(x => x.User.FirstName + " " + x.User.LastName));


        CreateMap<CreateAccountRequest, Account>();
        CreateMap<Account, AccountResponse>()
            .ForMember(dest => dest.UserName,
                src => src.MapFrom(x => x.User.FirstName + " " + x.User.LastName));

        CreateMap<CreateDocumentRequest, Document>();
        CreateMap<Document, DocumentResponse>();

        CreateMap<CreateExpenceNotifyRequest, ExpenceNotify>();
        CreateMap<ExpenceNotify, ExpenceNotifyResponse>()
            .ForMember(dest => dest.UserName,
                src => src.MapFrom(x => x.User.FirstName + " " + x.User.LastName))
            .ForMember(dest => dest.ExpenceType,
                src => src.MapFrom(x => x.ExpenceType.Name));

        CreateMap<CreateExpencePaymentRequest, ExpencePayment>();
        CreateMap<ExpencePayment, ExpencePaymentResponse>()
            .ForMember(dest => dest.AccountName,
                src => src.MapFrom(x => x.Account.Name ));

        CreateMap<CreateExpenceRespondRequest, ExpenceRespond>();
        CreateMap<ExpenceRespond, ExpenceRespondResponse>()
            .ForMember(dest => dest.UserName,
                src => src.MapFrom(x => x.User.FirstName + " " + x.User.LastName));

        CreateMap<CreateUserRequest, User>();
        CreateMap<User, UserResponse>()
            .ForMember(dest => dest.Role,
                src => src.MapFrom(x => x.Role.Name));



    }
}