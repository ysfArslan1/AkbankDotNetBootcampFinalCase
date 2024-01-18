using FinalCase.Base.Schema;
using System.Text.Json.Serialization;

namespace FinalCase.Schema;

// ExpenceNotify s�n�f� i�in gelen requestleri almakta kullan�l�r.
public class ExpenceNotifyRequest : BaseRequest
{
    
    public int UserId { get; set; }
    public int ExpenceTypeId { get; set; }
    public string Explanation { get; set; }
    public decimal Amount { get; set; }
    public string TransferType { get; set; }
}

// ExpenceNotify s�n�f� i�in response g�nderilmekte kullan�l�r.
public class ExpenceNotifyResponse : BaseResponse
{
    public string UserName { get; set; }
    public int ExpenceTypeId { get; set; }
    public string Explanation { get; set; }
    public decimal Amount { get; set; }
    public string TransferType { get; set; }
}
