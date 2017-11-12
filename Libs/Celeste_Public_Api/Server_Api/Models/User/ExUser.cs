#region Using directives

using System.Collections.Generic;
using Celeste_Public_Api.Server_Api.Models.User.Enum;

#endregion

namespace Celeste_Public_Api.Server_Api.Models.User
{
    public class RemoteUser
    {
        public string Mail { get; set; }

        public string ProfileName { get; set; }

        public string Ip { get; set; }

        public Rank Rank { get; set; }

        public bool BannedGame { get; set; }

        public bool BannedChat { get; set; }

        public List<Friend> Friends { get; set; } = new List<Friend>();
    }
}