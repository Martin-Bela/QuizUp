using QuizUp.MAUI.ViewModels;

namespace QuizUp.MAUI.Views;

public partial class NextQuestionView : ContentPageBase
{
    public NextQuestionView(NextQuestionViewModel viewModel) : base(viewModel)
    {
        InitializeComponent();
    }
}