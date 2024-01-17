using FinalCase.Base.Schema;
using System.Text.Json.Serialization;

namespace FinalCase.Schema;

public class ContactRequest : BaseRequest
{
    [JsonIgnore]
    public int Id { get; set; }
    
    public int UserId { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
}


public class ContactResponse : BaseResponse
{
    [JsonIgnore]
    public int Id { get; set; }
    public int UserId { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
}
