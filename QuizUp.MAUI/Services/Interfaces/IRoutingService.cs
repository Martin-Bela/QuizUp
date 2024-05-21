using QuizUp.MAUI.Models;
using QuizUp.MAUI.ViewModels;

namespace QuizUp.MAUI.Services;

public interface IRoutingService
{
    IList<RouteModel> Routes { get; }

    string GetRouteByViewModel<TViewModel>()
        where TViewModel : IViewModel;
}
