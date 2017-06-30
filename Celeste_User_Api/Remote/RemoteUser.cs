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

        public RemoteUser(long id, string ip, string mail, string profileName, long xuid, Rank rank,
            string authToken, bool isConnectedCelesteServer,
            string serverIp, bool bannedGame, bool isConnectedGameServer, bool bannedChat,
            bool isConnectedCustomChatServer, IEnumerable<Civilization> civilizations, List<Friend> friends,
            List<Invite> invites)
        {
            Id = id;
            Ip = ip;
            Mail = mail;
            ProfileName = profileName;
            Xuid = xuid;
            Rank = rank;
            AuthToken = authToken;
            IsConnectedCelesteServer = isConnectedCelesteServer;
            ServerIp = serverIp;
            BannedGame = bannedGame;
            IsConnectedGameServer = isConnectedGameServer;
            BannedChat = bannedChat;
            IsConnectedCustomChatServer = isConnectedCustomChatServer;
            AllowedCiv = new List<Civilization>();
            foreach (var civ in civilizations)
                AllowedCiv.Add(civ);
            Friends = friends;
            Invites = invites;
        }

        public long Id { get; set; }

        public string Ip { get; set; }

        public string Mail { get; set; }

        public string ProfileName { get; set; }

        public long Xuid { get; set; }

        public Rank Rank { get; set; }

        public string AuthToken { get; set; }

        public bool IsConnectedCelesteServer { get; set; }

        public string ServerIp { get; set; }

        public bool BannedGame { get; set; }

        public bool IsConnectedGameServer { get; set; }

        public bool BannedChat { get; set; }

        public bool IsConnectedCustomChatServer { get; set; }

        public List<Civilization> AllowedCiv { get; set; } = new List<Civilization>();

        public List<Friend> Friends { get; set; } = new List<Friend>();

        public List<Invite> Invites { get; set; } = new List<Invite>();
    }

    public class RemoteUserCharA
    {
        public RemoteUserCharA(long id, Civilization civilization)
        {
            Id = id;
            Civilization = civilization;
        }

        public long Id { get; set; }

        public Civilization Civilization { get; set; }
    }
}