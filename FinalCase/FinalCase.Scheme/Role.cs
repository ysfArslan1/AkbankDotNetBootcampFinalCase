using FinalCase.Base.Schema;
using System.Text.Json.Serialization;

namespace FinalCase.Schema;

// Role s�n�f� i�in gelen requestleri almakta kullan�l�r.
public class RoleRequest : BaseRequest
{
    public string Name { get; set; }
}

// Role s�n�f� i�in response g�nderilmekte kullan�l�r.
public class RoleResponse : BaseResponse
{
    public string Name { get; set; }
}
