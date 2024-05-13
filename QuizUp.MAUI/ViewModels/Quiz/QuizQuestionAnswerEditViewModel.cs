using CommunityToolkit.Mvvm.Input;

namespace QuizUp.MAUI.ViewModels;

public partial class QuizQuestionAnswerEditViewModel(ViewModelBase.Dependencies dependencies) : ViewModelBase(dependencies)
{
    [RelayCommand]
    private async Task SaveAnswerAsync()
    {
        await Shell.Current.GoToAsync("../");
    }
}
