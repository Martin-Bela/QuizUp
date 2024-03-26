using QuizUp.MAUI.ViewModels;

namespace QuizUp.MAUI.Views;

public partial class QuestionView : ContentPageBase
{
    public QuestionView() : base(new QuestionViewModel() /*todo: use DI*/)
    {
        InitializeComponent();
    }
}