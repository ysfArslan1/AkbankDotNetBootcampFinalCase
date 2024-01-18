using FinalCase.Base.Schema;
using System.Text.Json.Serialization;

namespace FinalCase.Schema;

// Document s�n�f� i�in gelen create requestlerini almakta kullan�l�r.
public class CreateDocumentRequest : BaseRequest
{
    
    public int ExpenceNotifyId { get; set; }
    public string Description { get; set; }
    public byte[] Content { get; set; }
}
// Document s�n�f� i�in gelen update requestlerini almakta kullan�l�r.
public class UpdateDocumentRequest : BaseRequest
{

    public string Description { get; set; }
    public byte[] Content { get; set; }
}

// Document s�n�f� i�in response g�nderilmekte kullan�l�r.
public class DocumentResponse : BaseResponse
{
    public int ExpenceNotifyId { get; set; }
    public string Description { get; set; }
    public byte[] Content { get; set; }
}
