using IdentityModel.Client;
using QuizUp.MAUI.Services;
using System.Net;

namespace QuizUp.MAUI.Api;

public class AccessTokenDelegatingHandler(ITokenHandler tokenHandler) : DelegatingHandler
{
    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        // access token is not needed for user registration
        if (request.RequestUri?.AbsolutePath == "/api/users" && request.Method == HttpMethod.Post)
        {
            return await base.SendAsync(request, cancellationToken);
        }

        var accessToken = await tokenHandler.TryGetAccessTokenAsync();
        if (accessToken == null)
        {
            return new HttpResponseMessage(HttpStatusCode.Unauthorized);
        }

        request.SetBearerToken(accessToken);

        return await base.SendAsync(request, cancellationToken);
    }
}
