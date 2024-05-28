using IdentityModel.Client;
using IdentityModel.OidcClient.Browser;
using QuizUp.Common;

namespace QuizUp.MAUI.Services;

public class AuthenticationWebBrowser : IdentityModel.OidcClient.Browser.IBrowser
{
    public async Task<BrowserResult> InvokeAsync(BrowserOptions options, CancellationToken cancellationToken = default)
    {
        try
        {
#if WINDOWS
            var result = await WinUIEx.WebAuthenticator.AuthenticateAsync(new Uri(options.StartUrl), new Uri(options.EndUrl), cancellationToken);
#else
            var result = await Microsoft.Maui.Authentication.WebAuthenticator.AuthenticateAsync(new Uri(options.StartUrl), new Uri(options.EndUrl));
#endif
            var responseUrl = new RequestUrl(AppConfig.MAUI.LoginRedirectUri)
                .Create(new Parameters(result.Properties));

            return new BrowserResult()
            {
                ResultType = BrowserResultType.Success,
                Response = responseUrl
            };
        }
        catch (Exception e)
        {
            return new BrowserResult()
            {
                ResultType = BrowserResultType.UnknownError,
                Error = e.Message
            };
        }
    }
}