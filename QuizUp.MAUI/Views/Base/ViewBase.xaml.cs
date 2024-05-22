using QuizUp.MAUI.ViewModels;

namespace QuizUp.MAUI.Views;

public abstract partial class ViewBase : ContentPage
{
    protected IViewModel ViewModel { get; }

    protected ViewBase(IViewModel viewModel)
    {
        ViewModel = viewModel;
        BindingContext = viewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        await ViewModel.OnAppearingAsync();
    }
}