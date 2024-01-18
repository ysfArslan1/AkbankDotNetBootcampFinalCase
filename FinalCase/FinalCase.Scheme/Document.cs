using FinalCase.Base.Schema;
using System.Text.Json.Serialization;

namespace FinalCase.Schema;

// Document sýnýfý için gelen requestleri almakta kullanýlýr.
public class DocumentRequest : BaseRequest
{
    
    public int ExpenceNotifyId { get; set; }
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
