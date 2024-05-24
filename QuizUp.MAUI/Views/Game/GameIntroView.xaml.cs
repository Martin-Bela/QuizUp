using QuizUp.MAUI.ViewModels;

namespace QuizUp.MAUI.Views;

public partial class GameIntroView : ViewBase
{
    public GameIntroView(GameIntroViewModel viewModel) : base(viewModel)
    {
        InitializeComponent();
    }
}