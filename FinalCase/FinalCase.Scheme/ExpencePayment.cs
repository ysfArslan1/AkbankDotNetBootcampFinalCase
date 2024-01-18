using FinalCase.Base.Schema;
using System.Text.Json.Serialization;

namespace FinalCase.Schema;

// ExpencePayment sýnýfý için gelen requestleri almakta kullanýlýr.
public class ExpencePaymentRequest : BaseRequest
{
    
    public int ExpenceRespondId { get; set; }
    public int AccountId { get; set; }
    public string Description { get; set; }
    public string TransferType { get; set; }
    public DateTime TransactionDate { get; set; }
    public bool IsDeposited { get; set; }
}

// ExpencePayment sýnýfý için response gönderilmekte kullanýlýr.
public class ExpencePaymentResponse : BaseResponse
{
    public int ExpenceRespondId { get; set; }
    public int AccountId { get; set; }
    public string Description { get; set; }
    public string TransferType { get; set; }
    public DateTime TransactionDate { get; set; }
    public bool IsDeposited { get; set; }
}
