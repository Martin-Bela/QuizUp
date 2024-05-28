using QuizUp.MAUI.ViewModels;

namespace QuizUp.MAUI.Views;

public partial class ProfileView : ViewBase
{
	public ProfileView(ProfileViewModel profileViewModel) : base(profileViewModel)
	{
		InitializeComponent();
	}
}