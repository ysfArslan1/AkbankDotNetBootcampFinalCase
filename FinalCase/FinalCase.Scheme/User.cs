using FinalCase.Base.Schema;
using System.Text.Json.Serialization;

namespace FinalCase.Schema;

// User s�n�f� i�in gelen requestleri almakta kullan�l�r.
public class UserRequest : BaseRequest
{

    public string IdentityNumber { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string daDateOfBirtht { get; set; }
    public string LastActivityDate { get; set; }
    public int RoleId { get; set; }
}

// User s�n�f� i�in response g�nderilmekte kullan�l�r.
public class UserResponse : BaseResponse
{
    public string IdentityNumber { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string daDateOfBirtht { get; set; }
    public string LastActivityDate { get; set; }
    public string Role { get; set; }
}
