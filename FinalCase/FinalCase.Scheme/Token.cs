using FinalCase.Base.Schema;

namespace FinalCase.Schema;

public class TokenRequest : BaseRequest
{
    public string Email { get; set; }
    public string Password { get; set; }
}

public class TokenResponse : BaseResponse
{
    public DateTime ExpireDate { get; set; }
    public string Token { get; set; }
    public string Email { get; set; }
}