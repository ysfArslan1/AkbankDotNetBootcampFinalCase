using FinalCase.Base.Schema;
using System.Text.Json.Serialization;

namespace FinalCase.Schema;

// User sýnýfý için gelen requestleri almakta kullanýlýr.
public class UserRequest : BaseRequest
{

    public string IdentityNumber { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string daDateOfBirtht { get; set; }
    public string LastActivityDate { get; set; }
    public int RoleId { get; set; }
}

// User sýnýfý için response gönderilmekte kullanýlýr.
public class UserResponse : BaseResponse
{
    public string IdentityNumber { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string daDateOfBirtht { get; set; }
    public string LastActivityDate { get; set; }
    public string Role { get; set; }
}
