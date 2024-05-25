namespace QuizUp.Common;

public static class AppConfig
{
    public static class Common
    {
        public static string DbConnectionString
        {
            get
            {
                var localApplicationDataPath = Environment.SpecialFolder.LocalApplicationData;
                var databaseFilePath = Path.Join(Environment.GetFolderPath(localApplicationDataPath), "quizup.db");
                return $"Data Source={databaseFilePath}";
            }
        }
    }

    public static class Server
    {
        public const string BaseUrl = "https://localhost:7126";

        public const string ApiScopeName = "quizup-api";

        public const string ApiScopeDisplayName = "QuizUp API";

        public const string SwaggerClientId = "quizup-swagger";

        public const string SwaggerClientSecret = "49C1A7E1-0C79-4A89-A3D6-A37998FB86B0";

        public const string SwaggerLoginRedirectUri = $"{BaseUrl}/swagger/oauth2-redirect.html";
    }

    public static class IdentityServer
    {
        public const string BaseUrl = "https://d3d2-109-183-191-100.ngrok-free.app/"; // "https://localhost:5001";

        public const string AuthorizationUrl = $"{BaseUrl}/connect/authorize";

        public const string TokenUrl = $"{BaseUrl}/connect/token";

        public const string IdentityScopes = "openid profile user-info";

        public const string OfflineAccessScope = "offline_access";
    }

    public static class MAUI
    {
        public const string ClientId = "quizup-maui";

        public const string ClientSecret = "49C1A7E1-0C79-4A89-A3D6-A37998FB86B0";

        public const string LoginCallbackScheme = "quizup-app";

        public const string LoginRedirectUri = $"{LoginCallbackScheme}://";
    }
}
