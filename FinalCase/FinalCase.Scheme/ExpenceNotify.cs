using FinalCase.Base.Schema;
using System.Text.Json.Serialization;

namespace FinalCase.Schema;

// ExpenceNotify s�n�f� i�in gelen create requestlerini almakta kullan�l�r.
public class CreateExpenceNotifyRequest : BaseRequest
{
    
    public int UserId { get; set; }
    public int ExpenceTypeId { get; set; }
    public string Explanation { get; set; }
    public decimal Amount { get; set; }
    public string TransferType { get; set; }
}
// ExpenceNotify s�n�f� i�in gelen update requestlerini almakta kullan�l�r.
public class UpdateExpenceNotifyRequest : BaseRequest
{

    public int ExpenceTypeId { get; set; }
    public string Explanation { get; set; }
    public decimal Amount { get; set; }
    public string TransferType { get; set; }
}

// ExpenceNotify s�n�f� i�in response g�nderilmekte kullan�l�r.
public class ExpenceNotifyResponse : BaseResponse
{
    public string UserName { get; set; }
    public string ExpenceType { get; set; }
    public string Explanation { get; set; }
    public decimal Amount { get; set; }
    public string TransferType { get; set; }
}
