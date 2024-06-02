using System.Diagnostics;
using System.Globalization;

namespace QuizUp.MAUI.Converters;
public class AnswerStatisticsColorConverter : IValueConverter
{
    public object Convert(object? isAnswerCorrect, Type targetType, object? parameter, CultureInfo culture)
    {
        Debug.Assert(isAnswerCorrect != null && Application.Current != null);

        var isAnswerCorrectBool = (bool)isAnswerCorrect;

        var colorName = isAnswerCorrectBool ? "CorrectAnswerColor" : "WrongAnswerColor";

        Application.Current.Resources.TryGetValue(colorName, out var colorObject);

        var color = (Color)colorObject;

        return color;
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
