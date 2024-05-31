﻿using System.Diagnostics;
using System.Globalization;

namespace QuizUp.MAUI.Converters;
public class ButtonColorConverter : IValueConverter
{
    public object Convert(object? value, Type targetType, object? buttonIndex, CultureInfo culture)
    {
        Debug.Assert(buttonIndex != null && Application.Current != null);

        var parsingSuceeded = int.TryParse(buttonIndex.ToString(), out var buttonIndexInt);
        if (!parsingSuceeded)
        {
            buttonIndexInt = -1;
        }

        var colorName = ButtonIndexToColorName(buttonIndexInt);

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