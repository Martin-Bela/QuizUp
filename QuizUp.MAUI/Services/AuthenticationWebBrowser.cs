﻿using IdentityModel.Client;
using IdentityModel.OidcClient.Browser;
using QuizUp.Common;

namespace QuizUp.MAUI.Services;

public class AuthenticationWebBrowser : IdentityModel.OidcClient.Browser.IBrowser
{
    public async Task<BrowserResult> InvokeAsync(BrowserOptions options, CancellationToken cancellationToken = default)
    {
        try
        {
            var webAuthenticatorResult = await WebAuthenticator.AuthenticateAsync(new Uri(options.StartUrl), new Uri(options.EndUrl));

            var responseUrl = new RequestUrl(AppConfig.MAUI.LoginRedirectUri)
                .Create(new Parameters(webAuthenticatorResult.Properties));

            return new BrowserResult()
            {
                ResultType = BrowserResultType.Success,
                Response = responseUrl
            };
        } catch (Exception e) 
        {
            return new BrowserResult()
            {
                ResultType = BrowserResultType.UnknownError,
                Error = e.Message
            };
        }
    }
}
