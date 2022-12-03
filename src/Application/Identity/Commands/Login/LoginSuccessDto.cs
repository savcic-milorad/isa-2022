namespace TransfusionAPI.Application.Identity.Commands.Login;

public class LoginSuccessDto
{
    public string AccessToken { get; private set; }

    private LoginSuccessDto(string accessToken)
    {
        AccessToken = accessToken;
    }

    public static LoginSuccessDto From(string accessToken)
    {
        return new LoginSuccessDto(accessToken);
    }
}
