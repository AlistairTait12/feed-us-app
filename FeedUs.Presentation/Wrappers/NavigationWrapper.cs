
namespace FeedUs.Presentation.Wrappers;

public class NavigationWrapper : INavigationWrapper
{
    public async Task GoToAsync(string uri) =>
        await Shell.Current.GoToAsync(uri);

    public async Task GoToAsync(string uri, Dictionary<string, object> parameters) =>
        await Shell.Current.GoToAsync(uri, parameters);

    public async Task<bool> DisplayAlert(string title, string message, string accept, string cancel) =>
        await Shell.Current.DisplayAlert(title, message, accept, cancel);
}
