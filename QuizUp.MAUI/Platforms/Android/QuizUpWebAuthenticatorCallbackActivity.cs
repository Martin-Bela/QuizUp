﻿using Android.App;
using Android.Content;
using Android.Content.PM;
using QuizUp.Common;

namespace QuizUp.MAUI;

[Activity(NoHistory = true, LaunchMode = LaunchMode.SingleTop, Exported = true)]
[IntentFilter(
    [Intent.ActionView],
    Categories = [Intent.CategoryDefault, Intent.CategoryBrowsable],
    DataScheme = CALLBACK_SCHEME
)]
public class QuizUpWebAuthenticatorCallbackActivity : WebAuthenticatorCallbackActivity
{
    const string CALLBACK_SCHEME = AppConfig.MAUI.LoginCallbackScheme;
}
