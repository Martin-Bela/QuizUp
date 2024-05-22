using QuizUp.MAUI.ViewModels;

namespace QuizUp.MAUI.Views;

public partial class QuizQuestionAnswerEditView : ViewBase
{
	public QuizQuestionAnswerEditView(QuizQuestionAnswerEditViewModel quizQuestionAnswerEditViewModel) : base(quizQuestionAnswerEditViewModel)
	{
		InitializeComponent();
	}
}