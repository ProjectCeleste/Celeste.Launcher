#region Using directives

using System.Collections.Generic;
using Celeste_User.Enum;

#endregion

namespace Celeste_User.Remote
{
    public class RemoteUser
    {
        public RemoteUser()
        {
        }

        public RemoteUser(string ip, string mail, string profileName, Rank rank,
            bool bannedGame, bool bannedChat, List<Friend> friends)
        {
            Ip = ip;
            Mail = mail;
            ProfileName = profileName;
            Rank = rank;
            BannedGame = bannedGame;
            BannedChat = bannedChat;
            Friends = friends;
        }

        public string Mail { get; set; }

        public string ProfileName { get; set; }

        public string Ip { get; set; }

        public Rank Rank { get; set; }

        public bool BannedGame { get; set; }

        public bool BannedChat { get; set; }

        public List<Friend> Friends { get; set; } = new List<Friend>();
    }
}