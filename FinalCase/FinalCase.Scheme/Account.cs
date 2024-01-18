using FinalCase.Base.Schema;
using System.Text.Json.Serialization;

namespace FinalCase.Schema;

// Account sýnýfý için gelen create requestleri almakta kullanýlýr.
public class CreateAccountRequest : BaseRequest
{
    
    public int UserId { get; set; }
    public decimal Balance { get; set; }
    public string CurrencyType { get; set; }
    public string Name { get; set; }
}
// Account sýnýfý için gelen update requestleri almakta kullanýlýr.
public class UpdateAccountRequest : BaseRequest
{
    public decimal Balance { get; set; }
    public string Name { get; set; }
}

// Account sýnýfý için response gönderilmekte kullanýlýr.
public class AccountResponse : BaseResponse
{

    public string UserName { get; set; }
    public string AccountNumber { get; set; }
    public string IBAN { get; set; }
    public decimal Balance { get; set; }
    public string CurrencyType { get; set; }
    public string Name { get; set; }
}
