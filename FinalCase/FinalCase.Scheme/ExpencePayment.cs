using FinalCase.Base.Schema;
using System.Text.Json.Serialization;

namespace FinalCase.Schema;

// ExpencePayment s�n�f� i�in gelen requestleri almakta kullan�l�r.
public class ExpencePaymentRequest : BaseRequest
{
    
    public int ExpenceRespondId { get; set; }
    public int AccountId { get; set; }
    public string Description { get; set; }
    public string TransferType { get; set; }
    public DateTime TransactionDate { get; set; }
    public bool IsDeposited { get; set; }
}

// ExpencePayment s�n�f� i�in response g�nderilmekte kullan�l�r.
public class ExpencePaymentResponse : BaseResponse
{
    public int ExpenceRespondId { get; set; }
    public int AccountId { get; set; }
    public string AccountName { get; set; }
    public string Description { get; set; }
    public string TransferType { get; set; }
    public DateTime TransactionDate { get; set; }
    public bool IsDeposited { get; set; }
}
