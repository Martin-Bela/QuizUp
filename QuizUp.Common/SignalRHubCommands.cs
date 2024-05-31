namespace QuizUp.Common;

public static class SignalRHubCommands
{
    public const string GameCreated = "GameCreated";
    public const string GameJoined = "GameJoined";
    public const string GameStarted = "GameStarted";
    public const string NextQuestion = "NextQuestion";
    public const string AnswerAccepted = "AnswerAccepted";
    public const string Score = "Score";
    public const string GameError = "GameError";
}
