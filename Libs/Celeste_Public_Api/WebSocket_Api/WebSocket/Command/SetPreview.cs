#region Using directives

using System;
using System.Threading.Tasks;
using Celeste_Public_Api.WebSocket_Api.WebSocket.CommandInfo.Member;

#endregion

namespace Celeste_Public_Api.WebSocket_Api.WebSocket.Command
{
    public class SetPreview
    {
        public const string CmdName = "SETPREVIEW";

        private readonly DataExchange _dataExchange;

        public SetPreview(DataExchange dataExchange)
        {
            _dataExchange = dataExchange;
        }

        public async Task<SetPreviewResult> DoSetPreview(SetPreviewInfo request)
        {
            try
            {
                return await _dataExchange.DoDataExchange<SetPreviewResult, SetPreviewInfo>(request, CmdName);
            }
            catch (Exception e)
            {
                return new SetPreviewResult(false, e.Message);
            }
        }
    }
}
