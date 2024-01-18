using FinalCase.Base.Schema;
using System.Text.Json.Serialization;

namespace FinalCase.Schema;

// ExpenceNotify sýnýfý için gelen requestleri almakta kullanýlýr.
public class ExpenceNotifyRequest : BaseRequest
{
    
    public int UserId { get; set; }
    public int ExpenceTypeId { get; set; }
    public string Explanation { get; set; }
    public decimal Amount { get; set; }
    public string TransferType { get; set; }
}

// ExpenceNotify sýnýfý için response gönderilmekte kullanýlýr.
public class ExpenceNotifyResponse : BaseResponse
{
    public string UserName { get; set; }
    public int ExpenceTypeId { get; set; }
    public string Explanation { get; set; }
    public decimal Amount { get; set; }
    public string TransferType { get; set; }
}
