using FinalCase.Base.Schema;
using System.Text.Json.Serialization;

namespace FinalCase.Schema;

// ExpenceRespond s�n�f� i�in gelen create requestlerin� almakta kullan�l�r.
public class CreateExpenceRespondRequest : BaseRequest
{
    
    public int UserId { get; set; }
    public int ExpenceNotifyId { get; set; }
    public string Explanation { get; set; }
    public bool isApproved { get; set; }
}
// ExpenceRespond s�n�f� i�in gelen update requestlerin� almakta kullan�l�r.
public class UpdateExpenceRespondRequest : BaseRequest
{

    public string Explanation { get; set; }
    public bool isApproved { get; set; }
}

// ExpenceRespond s�n�f� i�in response g�nderilmekte kullan�l�r.
public class ExpenceRespondResponse : BaseResponse
{
    public string UserName { get; set; }
    public int ExpenceNotifyId { get; set; }
    public string Explanation { get; set; }
    public bool isApproved { get; set; }
}
