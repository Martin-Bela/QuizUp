

using QuizUp.MAUI.ViewModels;

namespace QuizUp.MAUI.Views;

public abstract partial class ContentPageBase : ContentPage
{
    protected IViewModel ViewModel { get; }


    protected ContentPageBase(IViewModel viewModel)
    {
        this.ViewModel = viewModel;
        BindingContext = viewModel;
    }
}