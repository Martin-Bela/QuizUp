using QuizUp.MAUI.ViewModels;

namespace QuizUp.MAUI.Views;

public partial class GameResultsView : ViewBase
{
    public GameResultsView(GameResultsViewModel viewModel) : base(viewModel)
    {
        InitializeComponent();
    }
}