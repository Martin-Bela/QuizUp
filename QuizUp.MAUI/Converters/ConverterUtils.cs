namespace QuizUp.MAUI.Converters;

public static class ConverterUtils
{
    public static int TryParseIndex(object? index)
    {
        if (index == null)
        {
            return -1;
        }

        var parsingSucceeded = int.TryParse(index.ToString(), out var indexInt);
        return parsingSucceeded ? indexInt : -1;
    }

    public static string AnswerIndexToColorName(int index) => index switch
    {
        0 => "FirstAnswerColor",
        1 => "SecondAnswerColor",
        2 => "ThirdAnswerColor",
        3 => "FourthAnswerColor",
        _ => "DefaultAnswerColor",
    };
}
