using FinalCase.Base.Schema;
using System.Text.Json.Serialization;

namespace FinalCase.Schema;

// User s�n�f� i�in gelen create requestlerini almakta kullan�l�r.
public class CreateUserRequest : BaseRequest
{

    public string IdentityNumber { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime DateOfBirtht { get; set; }
    public DateTime LastActivityDate { get; set; }
    public int RoleId { get; set; }
}
// User s�n�f� i�in gelen update requestlerini almakta kullan�l�r.
public class UpdateUserRequest : BaseRequest
{

    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime DateOfBirtht { get; set; }
    public int RoleId { get; set; }
}

// User s�n�f� i�in response g�nderilmekte kullan�l�r.
public class UserResponse : BaseResponse
{
    public string IdentityNumber { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string DateOfBirtht { get; set; }
    public string LastActivityDate { get; set; }
    public string Role { get; set; }
}
