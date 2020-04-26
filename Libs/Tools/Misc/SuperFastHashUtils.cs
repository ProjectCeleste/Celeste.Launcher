#region Using directives

using System;

#endregion

//http://landman-code.blogspot.com/2009/02/c-superfasthash-and-murmurhash2.html

namespace ProjectCeleste.GameFiles.Tools.Misc
{
    public static class SuperFastHashUtils
    {
        public static uint GetSuperFastHash(this byte[] dataToHash)
        {
            var dataLength = dataToHash.Length;
            if (dataLength == 0)
                return 0;

            // CUSTOMIZED --> Starts with 0, not with datalen
            // var hash = Convert.ToUInt32(dataLength);
            uint hash = 0;
            var remainingBytes = dataLength & 3; // mod 4
            var numberOfLoops = dataLength >> 2; // div 4
            var currentIndex = 0;
            while (numberOfLoops > 0)
            {
                hash += BitConverter.ToUInt16(dataToHash, currentIndex);
                var tmp = (uint) (BitConverter.ToUInt16(dataToHash, currentIndex + 2) << 11) ^ hash;
                hash = (hash << 16) ^ tmp;
                hash += hash >> 11;
                currentIndex += 4;
                numberOfLoops--;
            }

            switch (remainingBytes)
            {
                case 3:
                    hash += BitConverter.ToUInt16(dataToHash, currentIndex);
                    hash ^= hash << 16;
                    hash ^= (uint) dataToHash[currentIndex + 2] << 18;

                    hash += hash >> 11;
                    break;
                case 2:
                    hash += BitConverter.ToUInt16(dataToHash, currentIndex);
                    hash ^= hash << 11;
                    hash += hash >> 17;
                    break;
                case 1:
                    hash += dataToHash[currentIndex];
                    hash ^= hash << 10;
                    hash += hash >> 1;
                    break;
                // ReSharper disable once RedundantEmptySwitchSection
                default:
                    break;
            }

            /* Force "avalanching" of final 127 bits */
            hash ^= hash << 3;
            hash += hash >> 5;
            // CUSTOMIZED --> Altered avalanching part
            hash ^= hash << 2;
            hash += hash >> 15;
            hash ^= hash << 10;

            // Old Part:
            // hash ^= hash << 4;
            // hash += hash >> 17;
            // hash ^= hash << 25;
            // hash += hash >> 6;

            return hash;
        }
    }
}