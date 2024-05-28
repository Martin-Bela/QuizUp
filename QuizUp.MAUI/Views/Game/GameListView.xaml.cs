using QuizUp.MAUI.ViewModels;

namespace QuizUp.MAUI.Views;

public partial class GameListView : ViewBase
{
    public GameListView(GameListViewModel viewModel) : base(viewModel)
    {
        InitializeComponent();
    }
}