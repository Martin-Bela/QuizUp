using CommunityToolkit.Mvvm.Input;

namespace QuizUp.MAUI.ViewModels;

public partial class QuizEditViewModel(ViewModelBase.Dependencies dependencies) : ViewModelBase(dependencies)
{
    [RelayCommand]
    private async Task GoToCreateQuestionAsync()
    {
        var route = routingService.GetRouteByViewModel<QuizQuestionEditViewModel>();
        await Shell.Current.GoToAsync(route);
    }

    [RelayCommand]
    private async Task GoBackAsync()
    {
        var route = routingService.GetRouteByViewModel<QuizListViewModel>();
        await Shell.Current.GoToAsync(route);
    }
}
