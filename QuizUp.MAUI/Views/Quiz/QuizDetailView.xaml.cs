using CommunityToolkit.Mvvm.Input;
using QuizUp.MAUI.ViewModels;

namespace QuizUp.MAUI.Views;

public partial class QuizDetailView : ViewBase
{
	public QuizDetailView(QuizDetailViewModel quizDetailViewModel) : base(quizDetailViewModel)
	{
		InitializeComponent();
	}
}