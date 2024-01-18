using FinalCase.Base.Schema;
using System.Text.Json.Serialization;

namespace FinalCase.Schema;

// Role sýnýfý için gelen requestleri almakta kullanýlýr.
public class RoleRequest : BaseRequest
{
    public string Name { get; set; }
}

// Role sýnýfý için response gönderilmekte kullanýlýr.
public class RoleResponse : BaseResponse
{
    public string Name { get; set; }
}
