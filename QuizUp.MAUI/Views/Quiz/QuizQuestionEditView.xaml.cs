using QuizUp.MAUI.ViewModels;

namespace QuizUp.MAUI.Views;

public partial class QuizQuestionEditView : ContentPageBase
{
	public QuizQuestionEditView(QuizQuestionEditViewModel quizQuestionEditViewModel) : base(quizQuestionEditViewModel)
	{
		InitializeComponent();
	}
}