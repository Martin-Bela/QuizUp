using System.Diagnostics;
using System.Globalization;

namespace QuizUp.MAUI.Converters;
public class AnswerFrameColorConverter : IValueConverter
{
    public object Convert(object? answerIndex, Type targetType, object? parameter, CultureInfo culture)
    {
        Debug.Assert(answerIndex != null && Application.Current != null);

        var parsingSuceeded = int.TryParse(answerIndex.ToString(), out var answerIndexInt);
        if (!parsingSuceeded)
        {
            answerIndexInt = -1;
        }

        var colorName = ButtonIndexToColorName(answerIndexInt);

        Application.Current.Resources.TryGetValue(colorName, out var colorValue);

        return colorValue;
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }

    private static string ButtonIndexToColorName(int index) => index switch
    {
        0 => "AnswerRed",
        1 => "AnswerBlue",
        2 => "AnswerYellow",
        3 => "AnswerGreen",
        _ => "AnswerDefaultColor"
    };
}
