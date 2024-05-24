using QuizUp.MAUI.Models;
using QuizUp.MAUI.ViewModels;
using QuizUp.MAUI.Views;

namespace QuizUp.MAUI.Services;

public interface IViewRoutingService
{
    IList<RouteModel> Routes { get; }

    string GetRouteByViewModel<TViewModel>()
        where TViewModel : IViewModel;

    string GetRouteByView<TView>()
        where TView : ViewBase;
}
