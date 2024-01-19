using FinalCase.Base.Schema;
using System.Text.Json.Serialization;

namespace FinalCase.Schema;

// ExpenceType sýnýfý için gelen create requestlerini almakta kullanýlýr.
public class CreateExpenceTypeRequest : BaseRequest
{
    
    public string Name { get; set; }
    public string Description { get; set; }
}

// ExpenceType sýnýfý için gelen update requestlerini almakta kullanýlýr.
public class UpdateExpenceTypeRequest : BaseRequest
{

    public string Name { get; set; }
    public string Description { get; set; }
}

// ExpenceType sýnýfý için response gönderilmekte kullanýlýr.
public class ExpenceTypeResponse : BaseResponse
{
    public string Name { get; set; }
    public string Description { get; set; }
}
