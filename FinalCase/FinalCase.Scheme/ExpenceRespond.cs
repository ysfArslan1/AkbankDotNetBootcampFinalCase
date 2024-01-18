using FinalCase.Base.Schema;
using System.Text.Json.Serialization;

namespace FinalCase.Schema;

// ExpenceRespond sýnýfý için gelen create requestleriný almakta kullanýlýr.
public class CreateExpenceRespondRequest : BaseRequest
{
    
    public int UserId { get; set; }
    public int ExpenceNotifyId { get; set; }
    public string Explanation { get; set; }
    public bool isApproved { get; set; }
}
// ExpenceRespond sýnýfý için gelen update requestleriný almakta kullanýlýr.
public class UpdateExpenceRespondRequest : BaseRequest
{

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
