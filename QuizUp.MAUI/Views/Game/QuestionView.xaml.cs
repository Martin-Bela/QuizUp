using QuizUp.MAUI.ViewModels;

namespace QuizUp.MAUI.Views;

public partial class QuestionView : ContentPageBase
{
    public QuestionView(QuestionViewModel questionViewModel) : base(questionViewModel)
    {
        InitializeComponent();
    }
}