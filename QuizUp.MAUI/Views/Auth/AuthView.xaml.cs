using QuizUp.MAUI.ViewModels;

namespace QuizUp.MAUI.Views;

public partial class AuthView : ViewBase
{
    public AuthView(AuthViewModel authViewModel) : base(authViewModel)
    {
        InitializeComponent();
    }
}
