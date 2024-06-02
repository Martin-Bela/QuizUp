using System.Diagnostics;
using System.Globalization;

namespace QuizUp.MAUI.Converters;
public class AnswerFrameColorConverter : IValueConverter
{
    public object Convert(object? answerIndex, Type targetType, object? parameter, CultureInfo culture)
    {
        Debug.Assert(Application.Current != null);

        var answerIndexInt = ConverterUtils.TryParseIndex(answerIndex);

        var colorName = ConverterUtils.AnswerIndexToColorName(answerIndexInt);

        Application.Current.Resources.TryGetValue(colorName, out var colorObject);

        var color = (Color)colorObject;

        return color;
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
