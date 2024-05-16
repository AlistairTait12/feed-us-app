using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using System.Diagnostics.CodeAnalysis;

namespace FeedUs.Presentation.ViewModels;

[ExcludeFromCodeCoverage]
public static class ViewModelStaticMethods
{
    public static async Task DisplayToast(string content)
    {
        var toast = Toast.Make(content, ToastDuration.Long, 14);
        await toast.Show();
    }
}
