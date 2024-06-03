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
        public const string BaseUrl = "https://d7b1-109-183-191-100.ngrok-free.app";

        public const string SignalRUrl = $"{BaseUrl}/quizHub";

        public const string ApiScopeName = "quizup-api";

        public const string ApiScopeDisplayName = "QuizUp API";

        public const string SwaggerClientId = "quizup-swagger";

        public const string SwaggerClientSecret = "49C1A7E1-0C79-4A89-A3D6-A37998FB86B0";

        public const string SwaggerLoginRedirectUri = $"{BaseUrl}/swagger/oauth2-redirect.html";
    }

    public static class IdentityServer
    {
        public const string BaseUrl = "https://6fc4-109-183-191-100.ngrok-free.app";

        public const string AuthorizationUrl = $"{BaseUrl}/connect/authorize";

        public const string TokenUrl = $"{BaseUrl}/connect/token";

        public const string IdentityScopes = "openid profile user-info";

        public const string OfflineAccessScope = "offline_access";

        // to-do: get those from discovery document
        public const string PublicKeyModulus = "rX38X7GpT0XSlgUEZOB4598_b4OPsf6Wt-VVaQU6D6fG0du7omzaPGhWMUS1SKta4s5GIf9VZgLVU-r4_3YaGQBYa9Qd3S0abTeg-50NR1TNRgB35GUdz7IIxE97QAfn2yfzb3cojDOYSKVAixaxodKGsoZDH5q_UgUuCRWfWPozNr8SAdNdVChDbwsCa2yJSqDobAST_5RQphgd1QvvgyrBn3_ZRroJQ10fw-Z8C7uAJE93eTkrubEYwWAGt1c1rRzmQgTyCK3NgHz36-36fh6cmqqv4CjFbNBGI5cdtZk4_18-MbTQpFtRq6YJL8TdXr3Lrr27gI3ZjUs_dgW2UQ";

        public const string PublicKeyExponent = "AQAB";

        public const string PublicKeyId = "64B3C08AC507ACF9BAA60B7DA347611D";
    }

    public static class MAUI
    {
        public const string ClientId = "quizup-maui";

        public const string ClientSecret = "49C1A7E1-0C79-4A89-A3D6-A37998FB86B0";

        public const string LoginCallbackScheme = "quizup-app";

        public const string LoginRedirectUri = $"{LoginCallbackScheme}://";

        public const string LogoutRedirectUri = $"{LoginCallbackScheme}://";
    }
}
