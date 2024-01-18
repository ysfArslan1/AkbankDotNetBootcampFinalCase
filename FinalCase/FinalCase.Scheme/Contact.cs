using FinalCase.Base.Schema;
using System.Text.Json.Serialization;

namespace FinalCase.Schema;

// Contact sýnýfý için gelen requestleri almakta kullanýlýr.
public class ContactRequest : BaseRequest
{
    [JsonIgnore]
    public int Id { get; set; }
    
    public int UserId { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
}

// Contact sýnýfý için response gönderilmekte kullanýlýr.
public class ContactResponse : BaseResponse
{
    [JsonIgnore]
    public int Id { get; set; }
    public int UserId { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
}
