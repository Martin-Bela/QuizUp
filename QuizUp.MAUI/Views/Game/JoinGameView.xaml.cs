using QuizUp.MAUI.ViewModels;

namespace QuizUp.MAUI.Views;

public partial class JoinGameView : ContentPageBase
{
    public JoinGameView(JoinGameViewModel joinGameViewModel) : base(joinGameViewModel)
    {
        InitializeComponent();
    }

    protected override bool OnBackButtonPressed()
    {
        return true;
    }
}