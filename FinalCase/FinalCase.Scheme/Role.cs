using FinalCase.Base.Schema;
using System.Text.Json.Serialization;

namespace FinalCase.Schema;

// Role s�n�f� i�in gelen create requestlerini almakta kullan�l�r.
public class CreateRoleRequest : BaseRequest
{
    public string Name { get; set; }
}

// Role s�n�f� i�in gelen update requestlerini almakta kullan�l�r.
public class UpdateRoleRequest : BaseRequest
{
    public string Name { get; set; }
}


// Role s�n�f� i�in response g�nderilmekte kullan�l�r.
public class RoleResponse : BaseResponse
{
    public string Name { get; set; }
}
