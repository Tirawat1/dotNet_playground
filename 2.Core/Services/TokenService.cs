namespace _2.Core;

public class TokenService : ITokenService
{
    private readonly IEmailProvider _emailProvider;
    public TokenService(IEmailProvider emailProvider){
        _emailProvider = emailProvider;
    }

    public async Task<String> SendEmailTokenAsync()
    {
        return await _emailProvider.SendEmailAsync(
            subject: "Seding a new token to you",
            message: "Please save the token xxxx",
            recepients: new List<string>(){"dummy@mail.com"}
        );
    }
}

// หน้านี้เป็นการสร้าง Token เพื่อยิงไป email ของผู้ใช้งาน