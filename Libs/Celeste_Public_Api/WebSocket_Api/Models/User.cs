#region Using directives

using System.Collections.Generic;
using Celeste_Public_Api.WebSocket_Api.Models.UserExt;
using Celeste_Public_Api.WebSocket_Api.Models.UserExt.Enum;

#endregion

namespace Celeste_Public_Api.WebSocket_Api.Models
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