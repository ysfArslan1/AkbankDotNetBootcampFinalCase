using FinalCase.Base.Schema;
using System.Text.Json.Serialization;

namespace FinalCase.Schema;

// Contact s�n�f� i�in gelen requestleri almakta kullan�l�r.
public class ContactRequest : BaseRequest
{
    [JsonIgnore]
    public int Id { get; set; }
    
    public int UserId { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
}

// Contact s�n�f� i�in response g�nderilmekte kullan�l�r.
public class ContactResponse : BaseResponse
{
    [JsonIgnore]
    public int Id { get; set; }
    public int UserId { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
}
