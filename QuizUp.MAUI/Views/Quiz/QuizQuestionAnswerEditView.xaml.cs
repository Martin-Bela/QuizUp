using QuizUp.MAUI.ViewModels;

namespace QuizUp.MAUI.Views;

public partial class QuizQuestionAnswerEditView : ContentPageBase
{
	public QuizQuestionAnswerEditView(QuizQuestionAnswerEditViewModel quizQuestionAnswerEditViewModel) : base(quizQuestionAnswerEditViewModel)
	{
		InitializeComponent();
	}
}