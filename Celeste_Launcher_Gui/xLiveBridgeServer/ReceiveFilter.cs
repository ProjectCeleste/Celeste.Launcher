#region Using directives

using SuperSocket.SocketBase.Protocol;

#endregion

namespace Celeste_Launcher_Gui.xLiveBridgeServer
{
    public class ReceiveFilter : ReceiveFilterBase<RequestInfo>
    {
        /// <summary>
        ///     Filters received data of the specific session into request info.
        /// </summary>
        /// <param name="readBuffer">The read buffer.</param>
        /// <param name="offset">The offset of the current received data in this read buffer.</param>
        /// <param name="length">The length of the current received data.</param>
        /// <param name="toBeCopied">if set to <c>true</c> [to be copied].</param>
        /// <param name="rest">The rest, the length of the data which hasn't been parsed.</param>
        /// <returns></returns>
        public override RequestInfo Filter(byte[] readBuffer, int offset, int length, bool toBeCopied, out int rest)
        {
            rest = 0;

            if (0 >= length) return null;

            var requestInfo = RequestInfo.FromByteArray(readBuffer, offset, length);

            return requestInfo;
        }
    }
}