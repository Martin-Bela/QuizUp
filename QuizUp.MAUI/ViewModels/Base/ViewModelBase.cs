using CommunityToolkit.Mvvm.ComponentModel;
using QuizUp.MAUI.Services;

namespace QuizUp.MAUI.ViewModels;

public abstract class ViewModelBase(ViewModelBase.Dependencies dependencies) : ObservableObject, IViewModel
{
    protected readonly IViewRoutingService routingService = dependencies.RoutingService;

    public virtual Task OnAppearingAsync()
    {
        return Task.CompletedTask;
    }

    public class Dependencies(IViewRoutingService routingService)
    {
        public IViewRoutingService RoutingService { get; } = routingService;
    }
}
