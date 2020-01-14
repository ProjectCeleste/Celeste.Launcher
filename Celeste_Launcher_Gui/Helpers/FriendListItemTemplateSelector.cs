#region Using directives

using Celeste_Launcher_Gui.ViewModels;
using System.Windows;
using System.Windows.Controls;

#endregion Using directives

namespace Celeste_Launcher_Gui.Helpers
{
    public class FriendListItemTemplateSelector : DataTemplateSelector
    {
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            var element = container as FrameworkElement;

            switch (item)
            {
                case OnlineFriend _:
                    return element?.FindResource("OnlineFriendTemplate") as DataTemplate;

                case OfflineFriend _:
                    return element?.FindResource("OfflineFriendTemplate") as DataTemplate;

                case OutgoingFriendRequest _:
                    return element?.FindResource("OutgoingFriendRequest") as DataTemplate;

                case IncomingFriendRequest _:
                    return element?.FindResource("IncomingFriendRequest") as DataTemplate;

                case FriendListSeparator _:
                    return element?.FindResource("Separator") as DataTemplate;

                default:
                    return null;
            }
        }
    }
}