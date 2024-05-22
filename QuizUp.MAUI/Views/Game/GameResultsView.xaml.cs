using QuizUp.MAUI.ViewModels;

namespace QuizUp.MAUI.Views;

public partial class GameResultsView : ViewBase
{
    internal GameResultsView(GameResultsViewModel viewModel) : base(viewModel)
    {
        InitializeComponent();
    }
}