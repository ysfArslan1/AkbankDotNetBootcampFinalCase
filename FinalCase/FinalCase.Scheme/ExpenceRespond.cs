using FinalCase.Base.Schema;
using System.Text.Json.Serialization;

namespace FinalCase.Schema;

// ExpenceRespond sýnýfý için gelen requestleri almakta kullanýlýr.
public class ExpenceRespondRequest : BaseRequest
{
    
    public int UserId { get; set; }
    public int ExpenceNotifyId { get; set; }
    public string Explanation { get; set; }
    public bool isApproved { get; set; }
}

// ExpenceRespond sýnýfý için response gönderilmekte kullanýlýr.
public class ExpenceRespondResponse : BaseResponse
{
    public string UserName { get; set; }
    public int ExpenceNotifyId { get; set; }
    public string Explanation { get; set; }
    public bool isApproved { get; set; }
}
