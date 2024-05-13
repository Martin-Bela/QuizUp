using QuizUp.MAUI.ViewModels;

namespace QuizUp.MAUI.Views;

public partial class QuizDetailView : ContentPageBase
{
	public QuizDetailView(QuizDetailViewModel quizDetailViewModel) : base(quizDetailViewModel)
	{
		InitializeComponent();
	}
}