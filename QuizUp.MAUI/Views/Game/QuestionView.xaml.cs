using QuizUp.MAUI.ViewModels;

namespace QuizUp.MAUI.Views;

public partial class QuestionView : ViewBase
{
    public QuestionView(QuestionViewModel questionViewModel) : base(questionViewModel)
    {
        InitializeComponent();
    }
}