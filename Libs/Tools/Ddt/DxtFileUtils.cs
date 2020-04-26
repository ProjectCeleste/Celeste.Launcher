#region Using directives

using System.Collections.Generic;
using System.IO;

#endregion

#region License

/* FNA - XNA4 Reimplementation for Desktop Platforms
 * Copyright 2009-2019 Ethan Lee and the MonoGame Team
 *
 * Released under the Microsoft Public License.
 * See LICENSE for details.
 */

//https://github.com/FNA-XNA/FNA/blob/master/src/Graphics/DxtUtil.cs

#endregion

namespace ProjectCeleste.GameFiles.Tools.Ddt
{
    internal static class DxtFileUtils
    {
        #region Internal Static Methods

        internal static byte[] DecompressDxt1(byte[] imageData, int width, int height)
        {
            using (var imageStream = new MemoryStream(imageData))
            {
                return DecompressDxt1(imageStream, width, height);
            }
        }

        internal static byte[] DecompressDxt1(Stream imageStream, int width, int height)
        {
            var imageData = new byte[width * height * 4];

            using (var imageReader = new BinaryReader(imageStream))
            {
                var blockCountX = (width + 3) / 4;
                var blockCountY = (height + 3) / 4;

                for (var y = 0; y < blockCountY; y++)
                for (var x = 0; x < blockCountX; x++)
                    DecompressDxt1Block(imageReader, x, y, blockCountX, width, height, imageData);
            }

            return imageData;
        }

        internal static byte[] DecompressDxt3(byte[] imageData, int width, int height)
        {
            using (var imageStream = new MemoryStream(imageData))
            {
                return DecompressDxt3(imageStream, width, height);
            }
        }

        internal static byte[] DecompressDxt3(Stream imageStream, int width, int height)
        {
            var imageData = new byte[width * height * 4];

            using (var imageReader = new BinaryReader(imageStream))
            {
                var blockCountX = (width + 3) / 4;
                var blockCountY = (height + 3) / 4;

                for (var y = 0; y < blockCountY; y++)
                for (var x = 0; x < blockCountX; x++)
                    DecompressDxt3Block(imageReader, x, y, blockCountX, width, height, imageData);
            }

            return imageData;
        }

        internal static byte[] DecompressDxt5(byte[] imageData, int width, int height)
        {
            using (var imageStream = new MemoryStream(imageData))
            {
                return DecompressDxt5(imageStream, width, height);
            }
        }

        internal static byte[] DecompressDxt5(Stream imageStream, int width, int height)
        {
            var imageData = new byte[width * height * 4];

            using (var imageReader = new BinaryReader(imageStream))
            {
                var blockCountX = (width + 3) / 4;
                var blockCountY = (height + 3) / 4;

                for (var y = 0; y < blockCountY; y++)
                for (var x = 0; x < blockCountX; x++)
                    DecompressDxt5Block(imageReader, x, y, blockCountX, width, height, imageData);
            }

            return imageData;
        }

        #endregion

        #region Private Static Methods

        private static void DecompressDxt1Block(BinaryReader imageReader, int x, int y, int blockCountX, int width,
            int height, IList<byte> imageData)
        {
            var c0 = imageReader.ReadUInt16();
            var c1 = imageReader.ReadUInt16();

            byte r0, g0, b0;
            byte r1, g1, b1;
            ConvertRgb565ToRgb888(c0, out r0, out g0, out b0);
            ConvertRgb565ToRgb888(c1, out r1, out g1, out b1);

            var lookupTable = imageReader.ReadUInt32();

            for (var blockY = 0; blockY < 4; blockY++)
            for (var blockX = 0; blockX < 4; blockX++)
            {
                byte r = 0, g = 0, b = 0, a = 255;
                var index = (lookupTable >> (2 * (4 * blockY + blockX))) & 0x03;

                if (c0 > c1)
                    switch (index)
                    {
                        case 0:
                            r = r0;
                            g = g0;
                            b = b0;
                            break;
                        case 1:
                            r = r1;
                            g = g1;
                            b = b1;
                            break;
                        case 2:
                            r = (byte) ((2 * r0 + r1) / 3);
                            g = (byte) ((2 * g0 + g1) / 3);
                            b = (byte) ((2 * b0 + b1) / 3);
                            break;
                        case 3:
                            r = (byte) ((r0 + 2 * r1) / 3);
                            g = (byte) ((g0 + 2 * g1) / 3);
                            b = (byte) ((b0 + 2 * b1) / 3);
                            break;
                        default:
                            break;
                    }
                else
                    switch (index)
                    {
                        case 0:
                            r = r0;
                            g = g0;
                            b = b0;
                            break;
                        case 1:
                            r = r1;
                            g = g1;
                            b = b1;
                            break;
                        case 2:
                            r = (byte) ((r0 + r1) / 2);
                            g = (byte) ((g0 + g1) / 2);
                            b = (byte) ((b0 + b1) / 2);
                            break;
                        case 3:
                            r = 0;
                            g = 0;
                            b = 0;
                            a = 0;
                            break;
                        default:
                            break;
                    }

                var px = (x << 2) + blockX;
                var py = (y << 2) + blockY;
                if (px >= width || py >= height) continue;
                var offset = (py * width + px) << 2;
                imageData[offset] = r;
                imageData[offset + 1] = g;
                imageData[offset + 2] = b;
                imageData[offset + 3] = a;
            }
        }

        private static void DecompressDxt3Block(BinaryReader imageReader, int x, int y, int blockCountX, int width,
            int height, IList<byte> imageData)
        {
            var a0 = imageReader.ReadByte();
            var a1 = imageReader.ReadByte();
            var a2 = imageReader.ReadByte();
            var a3 = imageReader.ReadByte();
            var a4 = imageReader.ReadByte();
            var a5 = imageReader.ReadByte();
            var a6 = imageReader.ReadByte();
            var a7 = imageReader.ReadByte();

            var c0 = imageReader.ReadUInt16();
            var c1 = imageReader.ReadUInt16();

            byte r0, g0, b0;
            byte r1, g1, b1;
            ConvertRgb565ToRgb888(c0, out r0, out g0, out b0);
            ConvertRgb565ToRgb888(c1, out r1, out g1, out b1);

            var lookupTable = imageReader.ReadUInt32();

            var alphaIndex = 0;
            for (var blockY = 0; blockY < 4; blockY++)
            for (var blockX = 0; blockX < 4; blockX++)
            {
                byte r = 0, g = 0, b = 0, a = 0;

                var index = (lookupTable >> (2 * (4 * blockY + blockX))) & 0x03;

                switch (alphaIndex)
                {
                    case 0:
                        a = (byte) ((a0 & 0x0F) | ((a0 & 0x0F) << 4));
                        break;
                    case 1:
                        a = (byte) ((a0 & 0xF0) | ((a0 & 0xF0) >> 4));
                        break;
                    case 2:
                        a = (byte) ((a1 & 0x0F) | ((a1 & 0x0F) << 4));
                        break;
                    case 3:
                        a = (byte) ((a1 & 0xF0) | ((a1 & 0xF0) >> 4));
                        break;
                    case 4:
                        a = (byte) ((a2 & 0x0F) | ((a2 & 0x0F) << 4));
                        break;
                    case 5:
                        a = (byte) ((a2 & 0xF0) | ((a2 & 0xF0) >> 4));
                        break;
                    case 6:
                        a = (byte) ((a3 & 0x0F) | ((a3 & 0x0F) << 4));
                        break;
                    case 7:
                        a = (byte) ((a3 & 0xF0) | ((a3 & 0xF0) >> 4));
                        break;
                    case 8:
                        a = (byte) ((a4 & 0x0F) | ((a4 & 0x0F) << 4));
                        break;
                    case 9:
                        a = (byte) ((a4 & 0xF0) | ((a4 & 0xF0) >> 4));
                        break;
                    case 10:
                        a = (byte) ((a5 & 0x0F) | ((a5 & 0x0F) << 4));
                        break;
                    case 11:
                        a = (byte) ((a5 & 0xF0) | ((a5 & 0xF0) >> 4));
                        break;
                    case 12:
                        a = (byte) ((a6 & 0x0F) | ((a6 & 0x0F) << 4));
                        break;
                    case 13:
                        a = (byte) ((a6 & 0xF0) | ((a6 & 0xF0) >> 4));
                        break;
                    case 14:
                        a = (byte) ((a7 & 0x0F) | ((a7 & 0x0F) << 4));
                        break;
                    case 15:
                        a = (byte) ((a7 & 0xF0) | ((a7 & 0xF0) >> 4));
                        break;
                    default:
                        break;
                }
                ++alphaIndex;

                switch (index)
                {
                    case 0:
                        r = r0;
                        g = g0;
                        b = b0;
                        break;
                    case 1:
                        r = r1;
                        g = g1;
                        b = b1;
                        break;
                    case 2:
                        r = (byte) ((2 * r0 + r1) / 3);
                        g = (byte) ((2 * g0 + g1) / 3);
                        b = (byte) ((2 * b0 + b1) / 3);
                        break;
                    case 3:
                        r = (byte) ((r0 + 2 * r1) / 3);
                        g = (byte) ((g0 + 2 * g1) / 3);
                        b = (byte) ((b0 + 2 * b1) / 3);
                        break;
                    default:
                        break;
                }

                var px = (x << 2) + blockX;
                var py = (y << 2) + blockY;
                if (px >= width || py >= height) continue;
                var offset = (py * width + px) << 2;
                imageData[offset] = r;
                imageData[offset + 1] = g;
                imageData[offset + 2] = b;
                imageData[offset + 3] = a;
            }
        }

        private static void DecompressDxt5Block(BinaryReader imageReader, int x, int y, int blockCountX, int width,
            int height, IList<byte> imageData)
        {
            var alpha0 = imageReader.ReadByte();
            var alpha1 = imageReader.ReadByte();

            var alphaMask = (ulong) imageReader.ReadByte();
            alphaMask += (ulong) imageReader.ReadByte() << 8;
            alphaMask += (ulong) imageReader.ReadByte() << 16;
            alphaMask += (ulong) imageReader.ReadByte() << 24;
            alphaMask += (ulong) imageReader.ReadByte() << 32;
            alphaMask += (ulong) imageReader.ReadByte() << 40;

            var c0 = imageReader.ReadUInt16();
            var c1 = imageReader.ReadUInt16();

            byte r0, g0, b0;
            byte r1, g1, b1;
            ConvertRgb565ToRgb888(c0, out r0, out g0, out b0);
            ConvertRgb565ToRgb888(c1, out r1, out g1, out b1);

            var lookupTable = imageReader.ReadUInt32();

            for (var blockY = 0; blockY < 4; blockY++)
            for (var blockX = 0; blockX < 4; blockX++)
            {
                byte r = 0, g = 0, b = 0, a = 255;
                var index = (lookupTable >> (2 * (4 * blockY + blockX))) & 0x03;

                var alphaIndex = (uint) ((alphaMask >> (3 * (4 * blockY + blockX))) & 0x07);
                switch (alphaIndex)
                {
                    case 0:
                        a = alpha0;
                        break;
                    case 1:
                        a = alpha1;
                        break;
                    default:
                        if (alpha0 > alpha1)
                            a = (byte) (((8 - alphaIndex) * alpha0 + (alphaIndex - 1) * alpha1) / 7);
                        else
                            switch (alphaIndex)
                            {
                                case 6:
                                    a = 0;
                                    break;
                                case 7:
                                    a = 0xff;
                                    break;
                                default:
                                    a = (byte) (((6 - alphaIndex) * alpha0 + (alphaIndex - 1) * alpha1) / 5);
                                    break;
                            }
                        break;
                }

                switch (index)
                {
                    case 0:
                        r = r0;
                        g = g0;
                        b = b0;
                        break;
                    case 1:
                        r = r1;
                        g = g1;
                        b = b1;
                        break;
                    case 2:
                        r = (byte) ((2 * r0 + r1) / 3);
                        g = (byte) ((2 * g0 + g1) / 3);
                        b = (byte) ((2 * b0 + b1) / 3);
                        break;
                    case 3:
                        r = (byte) ((r0 + 2 * r1) / 3);
                        g = (byte) ((g0 + 2 * g1) / 3);
                        b = (byte) ((b0 + 2 * b1) / 3);
                        break;
                    default:
                        break;
                }

                var px = (x << 2) + blockX;
                var py = (y << 2) + blockY;
                if (px >= width || py >= height) continue;
                var offset = (py * width + px) << 2;
                imageData[offset] = r;
                imageData[offset + 1] = g;
                imageData[offset + 2] = b;
                imageData[offset + 3] = a;
            }
        }

        private static void ConvertRgb565ToRgb888(ushort color, out byte r, out byte g, out byte b)
        {
            var temp = (color >> 11) * 255 + 16;
            r = (byte) ((temp / 32 + temp) / 32);
            temp = ((color & 0x07E0) >> 5) * 255 + 32;
            g = (byte) ((temp / 64 + temp) / 64);
            temp = (color & 0x001F) * 255 + 16;
            b = (byte) ((temp / 32 + temp) / 32);
        }

        #endregion
    }
}