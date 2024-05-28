﻿namespace QuizUp.Common;

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
        //public const string host = "localhost";
        //public const string BaseUrl = $"https://{host}:7126";
        public const string BaseUrl = "https://0040-62-169-187-240.ngrok-free.app";

        public const string SignalRUrl = $"{BaseUrl}/quizHub";

        public const string ApiScopeName = "quizup-api";

        public const string ApiScopeDisplayName = "QuizUp API";

        public const string SwaggerClientId = "quizup-swagger";

        public const string SwaggerClientSecret = "49C1A7E1-0C79-4A89-A3D6-A37998FB86B0";

        public const string SwaggerLoginRedirectUri = $"{BaseUrl}/swagger/oauth2-redirect.html";
    }

    public static class IdentityServer
    {
        //public const string BaseUrl = $"https://{Server.host}:5001";
        public const string BaseUrl = "https://4f79-62-169-187-240.ngrok-free.app";


        public const string AuthorizationUrl = $"{BaseUrl}/connect/authorize";

        public const string TokenUrl = $"{BaseUrl}/connect/token";

        public const string IdentityScopes = "openid profile user-info";

        public const string OfflineAccessScope = "offline_access";

        public const string PublicKey = "64B3C08AC507ACF9BAA60B7DA347611D";
    }

    public static class MAUI
    {
        public const string ClientId = "quizup-maui";

        public const string ClientSecret = "49C1A7E1-0C79-4A89-A3D6-A37998FB86B0";

        public const string LoginCallbackScheme = "quizup-app";

        public const string LoginRedirectUri = $"{LoginCallbackScheme}://";
    }
}
