﻿namespace QuizUp.Common.Models;

public class LoginUserResponseModel
{
    public required UserDetailModel User { get; set; }

    public required string Token { get; set; }
}