using CommunityToolkit.Mvvm.Input;

namespace QuizUp.MAUI.ViewModels;

public partial class QuizQuestionEditViewModel(ViewModelBase.Dependencies dependencies) : ViewModelBase(dependencies)
{
    [RelayCommand]
    private async Task GoToAnswerEditAsync()
    {
        await Shell.Current.GoToAsync("answer");
    }

    [RelayCommand]
    private async Task GoBackAsync()
    {
        await Shell.Current.GoToAsync("..");
    }
}
