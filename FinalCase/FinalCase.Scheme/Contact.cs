using FinalCase.Base.Schema;
using System.Text.Json.Serialization;

namespace FinalCase.Schema;

// Contact sýnýfý için gelen create requestlerini almakta kullanýlýr.
public class CreateContactRequest : BaseRequest
{
    
    public int UserId { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
}
// Contact sýnýfý için gelen update requestlerini almakta kullanýlýr.
public class UpdateContactRequest : BaseRequest
{

    public string Email { get; set; }
    public string PhoneNumber { get; set; }
}

// Contact sýnýfý için response gönderilmekte kullanýlýr.
public class ContactResponse : BaseResponse
{
    public int UserId { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
}
