using Celeste_Launcher_Gui.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace Celeste_Launcher_Gui.Helpers
{
    public class FriendListItemTemplateSelector : DataTemplateSelector
    {
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            FrameworkElement element = container as FrameworkElement;

            if (item is OnlineFriend)
                return element.FindResource("OnlineFriendTemplate") as DataTemplate;
            else if (item is OfflineFriend)
                return element.FindResource("OfflineFriendTemplate") as DataTemplate;
            else if (item is OutgoingFriendRequest)
                return element.FindResource("OutgoingFriendRequest") as DataTemplate;
            else if (item is IncomingFriendRequest)
                return element.FindResource("IncomingFriendRequest") as DataTemplate;
            else if (item is FriendListSeparator)
                return element.FindResource("Separator") as DataTemplate;

            return null;
        }
    }
}
