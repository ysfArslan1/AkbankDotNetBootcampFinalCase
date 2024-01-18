using FinalCase.Base.Schema;
using System.Text.Json.Serialization;

namespace FinalCase.Schema;

// Document sýnýfý için gelen create requestlerini almakta kullanýlýr.
public class CreateDocumentRequest : BaseRequest
{
    
    public int ExpenceNotifyId { get; set; }
    public string Description { get; set; }
    public byte[] Content { get; set; }
}
// Document sýnýfý için gelen update requestlerini almakta kullanýlýr.
public class UpdateDocumentRequest : BaseRequest
{

    public string Description { get; set; }
    public byte[] Content { get; set; }
}

// Document sýnýfý için response gönderilmekte kullanýlýr.
public class DocumentResponse : BaseResponse
{
    public int ExpenceNotifyId { get; set; }
    public string Description { get; set; }
    public byte[] Content { get; set; }
}
