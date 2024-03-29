using AutoMapper;
using FinalCase.Base.Encryption;
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

        CreateMap<CreateUserRequest, User>()
        .ForMember(dest => dest.Password, opt => opt.MapFrom(src => Md5Extension.GetHash(src.Password.Trim())));
        CreateMap<User, UserResponse>()
            .ForMember(dest => dest.Role,
                src => src.MapFrom(x => x.Role.Name));


        CreateMap<CreateExpenceTypeRequest, ExpenceType>();
        CreateMap<ExpenceType, ExpenceTypeResponse>();


        CreateMap<CreateRoleRequest, Role>();
        CreateMap<Role, RoleResponse>();

    }
}