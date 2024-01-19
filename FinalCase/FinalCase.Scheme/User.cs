using FinalCase.Base.Schema;
using System.Text.Json.Serialization;

namespace FinalCase.Schema;

// User sýnýfý için gelen create requestlerini almakta kullanýlýr.
public class CreateUserRequest : BaseRequest
{

    public string IdentityNumber { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime DateOfBirtht { get; set; }
    public DateTime LastActivityDate { get; set; }
    public int RoleId { get; set; }
}
// User sýnýfý için gelen update requestlerini almakta kullanýlýr.
public class UpdateUserRequest : BaseRequest
{

    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime DateOfBirtht { get; set; }
    public int RoleId { get; set; }
}

// User sýnýfý için response gönderilmekte kullanýlýr.
public class UserResponse : BaseResponse
{
    public string IdentityNumber { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string DateOfBirtht { get; set; }
    public string LastActivityDate { get; set; }
    public string Role { get; set; }
}
