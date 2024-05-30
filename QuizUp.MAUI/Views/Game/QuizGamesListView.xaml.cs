using QuizUp.MAUI.ViewModels;

namespace QuizUp.MAUI.Views;

public partial class QuizGamesListView : ViewBase
{
    public QuizGamesListView(QuizGamesListViewModel viewModel) : base(viewModel)
    {
        InitializeComponent();
    }
}