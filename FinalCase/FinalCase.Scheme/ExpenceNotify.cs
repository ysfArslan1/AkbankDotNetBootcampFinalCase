using FinalCase.Base.Schema;
using System.Text.Json.Serialization;

namespace FinalCase.Schema;

// ExpenceNotify sýnýfý için gelen create requestlerini almakta kullanýlýr.
public class CreateExpenceNotifyRequest : BaseRequest
{
    
    public int UserId { get; set; }
    public int ExpenceTypeId { get; set; }
    public string Explanation { get; set; }
    public decimal Amount { get; set; }
    public string TransferType { get; set; }
}
// ExpenceNotify sýnýfý için gelen update requestlerini almakta kullanýlýr.
public class UpdateExpenceNotifyRequest : BaseRequest
{

    public int ExpenceTypeId { get; set; }
    public string Explanation { get; set; }
    public decimal Amount { get; set; }
    public string TransferType { get; set; }
}

// ExpenceNotify sýnýfý için response gönderilmekte kullanýlýr.
public class ExpenceNotifyResponse : BaseResponse
{
    public string UserName { get; set; }
    public string ExpenceType { get; set; }
    public string Explanation { get; set; }
    public decimal Amount { get; set; }
    public string TransferType { get; set; }
}
