using QuizUp.MAUI.ViewModels;

namespace QuizUp.MAUI.Views;

public partial class QuizEditView : ViewBase
{
	public QuizEditView(QuizEditViewModel quizEditViewModel) : base(quizEditViewModel)
	{
		InitializeComponent();
	}
}