using QuizUp.Common.Models;
using QuizUp.MAUI.Resources.Fonts;
using System.Diagnostics;
using System.Globalization;

namespace QuizUp.MAUI.Converters;
public class AnswerResultIconConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        Debug.Assert(value != null);

        var parsingSuceeded = Enum.TryParse<AnswerResult>(value.ToString(), out var answerResult);
        if (!parsingSuceeded)
        {
            answerResult = (AnswerResult)(-1);
        }

        return AnswerResultToIcon(answerResult);
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
    private static string AnswerResultToIcon(AnswerResult result) => result switch
    {
        AnswerResult.Correct => FontAwesomeIcons.CircleCheck,
        AnswerResult.Incorrect => FontAwesomeIcons.CircleXmark,
        AnswerResult.TimeExpired => FontAwesomeIcons.CircleXmark,
        _ => FontAwesomeIcons.CircleXmark
    };
}