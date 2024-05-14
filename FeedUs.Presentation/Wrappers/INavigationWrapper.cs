namespace FeedUs.Presentation.Wrappers;

public interface INavigationWrapper
{
    Task GoToAsync(string uri);

    Task GoToAsync(string uri, Dictionary<string, object> parameters);

    Task<bool> DisplayAlert(string title, string message, string accept, string cancel);
}
