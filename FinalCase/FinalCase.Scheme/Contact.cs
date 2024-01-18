using FinalCase.Base.Schema;
using System.Text.Json.Serialization;

namespace FinalCase.Schema;

// Contact s�n�f� i�in gelen create requestlerini almakta kullan�l�r.
public class CreateContactRequest : BaseRequest
{
    
    public int UserId { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
}
// Contact s�n�f� i�in gelen update requestlerini almakta kullan�l�r.
public class UpdateContactRequest : BaseRequest
{

    public string Email { get; set; }
    public string PhoneNumber { get; set; }
}

// Contact s�n�f� i�in response g�nderilmekte kullan�l�r.
public class ContactResponse : BaseResponse
{
    public int UserId { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
}
