using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using FinalCase.Base.Encryption;
using FinalCase.Base.Response;
using FinalCase.Base.Token;
using FinalCase.Business.Cqrs;
using FinalCase.Data;
using FinalCase.Data.Entity;
using FinalCase.Schema;
using FinalCase.Data.DbOperations;

namespace FinalCase.Business.Command;

public class TokenCommandHandler :
    IRequestHandler<CreateTokenCommand, ApiResponse<TokenResponse>>
{
    private readonly VbDbContext dbContext;
    private readonly JwtConfig jwtConfig;

    public TokenCommandHandler(VbDbContext dbContext,IOptionsMonitor<JwtConfig> jwtConfig)
    {
        this.dbContext = dbContext;
        this.jwtConfig = jwtConfig.CurrentValue;
    }
    
    public async Task<ApiResponse<TokenResponse>> Handle(CreateTokenCommand request, CancellationToken cancellationToken)
    {
        var user = await dbContext.Set<User>().Where(x => x.Email == request.Model.Email).Include(x=>x.Role)
            .FirstOrDefaultAsync(cancellationToken);
        if (user == null)
        {
            return new ApiResponse<TokenResponse>("Invalid user information");
        }

        string hash = Md5Extension.GetHash(request.Model.Password.Trim());
        if (hash != user.Password)
        {
            user.LastActivityDate = DateTime.UtcNow;
            user.PasswordRetryCount++;
            await dbContext.SaveChangesAsync(cancellationToken);
            return new ApiResponse<TokenResponse>("Invalid user information");
        }

        if (user.IsActive == false)
        {
            return new ApiResponse<TokenResponse>("Invalid user status");
        }
        if (user.PasswordRetryCount > 3)
        {
            return new ApiResponse<TokenResponse>("Invalid user status");
        }
        
        user.LastActivityDate = DateTime.UtcNow;
        user.PasswordRetryCount = 0;
        await dbContext.SaveChangesAsync(cancellationToken);

        string token = Token(user);

        return new ApiResponse<TokenResponse>( new TokenResponse()
        {
            Email = user.Email,
            Token = token,
            ExpireDate =  DateTime.Now.AddMinutes(jwtConfig.AccessTokenExpiration)
        });
    }
    
    private string Token(User user)
    {
        Claim[] claims = GetClaims(user);
        var secret = Encoding.ASCII.GetBytes(jwtConfig.Secret);

        var jwtToken = new JwtSecurityToken(
            jwtConfig.Issuer,
            jwtConfig.Audience,
            claims,
            expires: DateTime.Now.AddMinutes(jwtConfig.AccessTokenExpiration),
            signingCredentials: new SigningCredentials(new SymmetricSecurityKey(secret), SecurityAlgorithms.HmacSha256Signature)
        );

        string accessToken = new JwtSecurityTokenHandler().WriteToken(jwtToken);
        return accessToken;
    }
    
    private Claim[] GetClaims(User user)
    {
        if (user == null)
        {
            // E�er user null ise, bo� bir dizi d�nd�rebilirsiniz veya ba�ka bir i�lem yapabilirsiniz.
            return Array.Empty<Claim>();
        }

        var claims = new List<Claim>
    {
        new Claim("Id", user.Id.ToString()),
        new Claim("Email", user.Email),
        new Claim("UserName", $"{user.FirstName} {user.LastName}")
    };

        if (user.Role != null)
        {
            claims.Add(new Claim(ClaimTypes.Role, user.Role.Name));
        }

        return claims.ToArray();


    }
}