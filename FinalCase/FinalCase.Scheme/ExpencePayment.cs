using FinalCase.Base.Schema;
using System.Text.Json.Serialization;

namespace FinalCase.Schema;

// ExpencePayment sýnýfý için gelen create requestleri almakta kullanýlýr.
public class CreateExpencePaymentRequest : BaseRequest
{
    
    public int ExpenceRespondId { get; set; }
    public int AccountId { get; set; }
    public int ReceiverId { get; set; }
    public string ReceiverName { get; set; }
    public DateTime TransactionDate { get; set; }
}

// ExpencePayment sýnýfý için gelen update requestleri almakta kullanýlýr.
public class UpdateExpencePaymentRequest : BaseRequest
{

    public string Description { get; set; }
    public DateTime TransactionDate { get; set; }
}

// ExpencePayment sýnýfý için response gönderilmekte kullanýlýr.
public class ExpencePaymentResponse : BaseResponse
{
    public int ExpenceRespondId { get; set; }
    public int AccountId { get; set; }
    public string AccountName { get; set; }
    public string ReceiverName { get; set; }
    public string Description { get; set; }
    public string TransferType { get; set; }
    public DateTime TransactionDate { get; set; }
}
