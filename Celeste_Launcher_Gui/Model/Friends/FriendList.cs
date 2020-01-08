using System.Collections.Generic;

namespace Celeste_Launcher_Gui.Model.Friends
{
    public class FriendList
    {
        public IList<Friend> Friends { get; set; }
        public IList<Friend> IncomingRequests { get; set; }
        public IList<Friend> OutgoingRequests { get; set; }
    }
}
