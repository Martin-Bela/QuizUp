using QuizUp.Common.Models;
using System.Diagnostics;
using System.Globalization;

namespace QuizUp.MAUI.Converters;
public class AnswerResultTextConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        Debug.Assert(value != null);

        var parsingSuceeded = Enum.TryParse<AnswerResult>(value.ToString(), out var answerResult);
        if (!parsingSuceeded)
        {
            answerResult = (AnswerResult)(-1);
        }

        return AnswerResultToText(answerResult);
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
    private static string AnswerResultToText(AnswerResult result) => result switch
    {
        AnswerResult.Correct => "Correct",
        AnswerResult.Incorrect => "Wrong answer",
        AnswerResult.TimeExpired => "Time is up",
        _ => "You are doing great!"
    };
}