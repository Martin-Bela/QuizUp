using QuizUp.MAUI.ViewModels;

namespace QuizUp.MAUI.Views;

public partial class QuizListView : ContentPageBase
{
	public QuizListView(QuizListViewModel quizListViewModel) : base(quizListViewModel)
	{
		InitializeComponent();
	}
}