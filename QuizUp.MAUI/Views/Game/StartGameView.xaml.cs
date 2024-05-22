using QuizUp.MAUI.ViewModels;

namespace QuizUp.MAUI.Views;

public partial class StartGameView : ViewBase
{
    internal StartGameView(StartGameViewModel viewModel) : base(viewModel)
    {
        InitializeComponent();
    }
}