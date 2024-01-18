using FinalCase.Base.Schema;
using System.Text.Json.Serialization;

namespace FinalCase.Schema;

// Account s�n�f� i�in gelen requestleri almakta kullan�l�r.
public class AccountRequest : BaseRequest
{
    
    public int UserId { get; set; }
    public decimal Balance { get; set; }
    public string CurrencyType { get; set; }
    public string Name { get; set; }
}

// Account s�n�f� i�in response g�nderilmekte kullan�l�r.
public class AccountResponse : BaseResponse
{

    public string UserName { get; set; }
    public string AccountNumber { get; set; }
    public string IBAN { get; set; }
    public decimal Balance { get; set; }
    public string CurrencyType { get; set; }
    public string Name { get; set; }
}
