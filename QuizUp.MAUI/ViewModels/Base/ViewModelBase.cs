using CommunityToolkit.Mvvm.ComponentModel;
using QuizUp.MAUI.Services;
using QuizUp.MAUI.Storage;

namespace QuizUp.MAUI.ViewModels;

public abstract class ViewModelBase(ViewModelBase.Dependencies dependencies) : ObservableObject, IViewModel
{
    protected readonly IViewRoutingService routingService = dependencies.RoutingService;

    protected readonly IUserDataStorage userDataStorage = dependencies.UserDataStorage;

    public virtual Task OnAppearingAsync()
    {
        return Task.CompletedTask;
    }

    public class Dependencies(IViewRoutingService routingService, IUserDataStorage userDataStorage)
    {
        public IViewRoutingService RoutingService { get; } = routingService;

        public IUserDataStorage UserDataStorage { get; } = userDataStorage;
    }
}
