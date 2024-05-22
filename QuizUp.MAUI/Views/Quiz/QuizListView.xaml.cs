using QuizUp.MAUI.ViewModels;

namespace QuizUp.MAUI.Views;

public partial class QuizListView : ViewBase
{
	public QuizListView(QuizListViewModel quizListViewModel) : base(quizListViewModel)
	{
		InitializeComponent();
	}
}