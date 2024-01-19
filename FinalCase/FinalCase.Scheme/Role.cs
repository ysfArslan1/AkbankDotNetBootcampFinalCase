using FinalCase.Base.Schema;
using System.Text.Json.Serialization;

namespace FinalCase.Schema;

// Role sýnýfý için gelen create requestlerini almakta kullanýlýr.
public class CreateRoleRequest : BaseRequest
{
    public string Name { get; set; }
}

// Role sýnýfý için gelen update requestlerini almakta kullanýlýr.
public class UpdateRoleRequest : BaseRequest
{
    public string Name { get; set; }
}


// Role sýnýfý için response gönderilmekte kullanýlýr.
public class RoleResponse : BaseResponse
{
    public string Name { get; set; }
}
