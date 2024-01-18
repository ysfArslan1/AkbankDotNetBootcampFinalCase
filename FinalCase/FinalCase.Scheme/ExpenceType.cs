using FinalCase.Base.Schema;
using System.Text.Json.Serialization;

namespace FinalCase.Schema;

// ExpenceType s�n�f� i�in gelen requestleri almakta kullan�l�r.
public class ExpenceTypeRequest : BaseRequest
{
    
    public string Name { get; set; }
    public string Description { get; set; }
}

// ExpenceType s�n�f� i�in response g�nderilmekte kullan�l�r.
public class ExpenceTypeResponse : BaseResponse
{
    public string Name { get; set; }
    public string Description { get; set; }
}
