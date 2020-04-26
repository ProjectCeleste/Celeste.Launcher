#region Using directives

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;

#endregion

namespace ProjectCeleste.GameFiles.Tools.Ddt
{
    public enum DdtFileTypeAlpha : byte
    {
        None = 0,
        Player = 1,
        Trans = 4,
        Blend = 8
    }

    public enum DdtFileTypeFormat : byte
    {
        Bgra = 1,
        Dxt1 = 4,
        Grey = 7,
        Dxt3 = 8,
        Dxt5 = 9
    }

    public enum DdtFileTypeUsage : byte
    {
        Unk0 = 0,
        Unk1 = 1,
        Bump = 6,
        Unk2 = 7,
        Cube = 8
    }

    public class DdtFile
    {
        public DdtFile(byte[] data)
        {
            using (var stream = new MemoryStream(data))
            {
                using (var binaryReader = new BinaryReader(stream))
                {
                    Head = new string(binaryReader.ReadChars(4));
                    Usage = (DdtFileTypeUsage) binaryReader.ReadByte();
                    Alpha = (DdtFileTypeAlpha) binaryReader.ReadByte();
                    Format = (DdtFileTypeFormat) binaryReader.ReadByte();
                    MipmapLevels = binaryReader.ReadByte();
                    BaseWidth = binaryReader.ReadInt32();
                    BaseHeight = binaryReader.ReadInt32();
                    var images = new List<DdtImage>();
                    var numImagesPerLevel = Usage == DdtFileTypeUsage.Cube ? 6 : 1;
                    for (var index = 0; index < MipmapLevels * numImagesPerLevel; ++index)
                    {
                        binaryReader.BaseStream.Position = 16 + 8 * index;
                        var width = BaseWidth >> (index / numImagesPerLevel);
                        if (width < 1)
                            width = 1;
                        var height = BaseHeight >> (index / numImagesPerLevel);
                        if (height < 1)
                            height = 1;
                        var offset = binaryReader.ReadInt32();
                        var length = binaryReader.ReadInt32();
                        binaryReader.BaseStream.Position = offset;
                        images.Add(new DdtImage(width, height, offset, length, binaryReader.ReadBytes(length)));
                    }
                    Images = new ReadOnlyCollection<DdtImage>(images);
                }
            }
            Bitmap = GetBitmap();
        }

        public string Head { get; }
        public DdtFileTypeUsage Usage { get; }
        public DdtFileTypeAlpha Alpha { get; }
        public DdtFileTypeFormat Format { get; }
        public byte MipmapLevels { get; }
        public int BaseWidth { get; }
        public int BaseHeight { get; }
        public IReadOnlyCollection<DdtImage> Images { get; }

        public Bitmap Bitmap { get; }

        public byte[] ToByteArray()
        {
            using (var ms = new MemoryStream())
            {
                using (var bw = new BinaryWriter(ms))
                {
                    bw.Write(Head);
                    bw.Write((byte) Usage);
                    bw.Write((byte) Alpha);
                    bw.Write((byte) Format);
                    bw.Write(MipmapLevels);
                    bw.Write(BaseWidth);
                    bw.Write(BaseHeight);
                    foreach (var image in Images)
                    {
                        bw.Write(image.Length);
                        bw.Write(image.Offset);
                    }
                    foreach (var image in Images)
                        bw.Write(image.RawData);
                    return ms.ToArray();
                }
            }
        }

        private Bitmap GetBitmap()
        {
            var ddtImage = Images.FirstOrDefault();
            if (ddtImage == null)
                return null;

            byte[] rawData;
            switch (Format)
            {
                case DdtFileTypeFormat.Bgra:
                {
                    var tmpRawData = ddtImage.RawData;
                    rawData = new byte[ddtImage.Width * ddtImage.Height * 4];
                    for (var i = 0; i < ddtImage.Width * ddtImage.Height; i++)
                    {
                        rawData[i * 4] = tmpRawData[i * 4 + 2];
                        rawData[i * 4 + 1] = tmpRawData[i * 4 + 1];
                        rawData[i * 4 + 2] = tmpRawData[i * 4];
                        rawData[i * 4 + 3] = tmpRawData[i * 4 + 3];
                    }
                    break;
                }
                case DdtFileTypeFormat.Grey:
                {
                    var tmpRawData = ddtImage.RawData;
                    rawData = new byte[ddtImage.Width * ddtImage.Height * 4];
                    for (var i = 0; i < ddtImage.Width * ddtImage.Height; i++)
                    {
                        rawData[i * 4] =
                            rawData[i * 4 + 1] =
                                rawData[i * 4 + 2] = tmpRawData[i];
                        rawData[i * 4 + 3] = 255;
                    }
                    break;
                }
                case DdtFileTypeFormat.Dxt1:
                {
                    rawData = DxtFileUtils.DecompressDxt1(ddtImage.RawData, ddtImage.Width, ddtImage.Height);
                    break;
                }
                case DdtFileTypeFormat.Dxt3:
                {
                    rawData = DxtFileUtils.DecompressDxt3(ddtImage.RawData, ddtImage.Width, ddtImage.Height);
                    break;
                }
                case DdtFileTypeFormat.Dxt5:
                {
                    var tmpRawData = DxtFileUtils.DecompressDxt5(ddtImage.RawData, ddtImage.Width, ddtImage.Height);
                    rawData = new byte[ddtImage.Width * ddtImage.Height * 4];
                    for (var i = 0; i < ddtImage.Width * ddtImage.Height; i++)
                    {
                        rawData[i * 4] = tmpRawData[i * 4 + 3];
                        rawData[i * 4 + 1] = tmpRawData[i * 4 + 1];
                        rawData[i * 4 + 2] = tmpRawData[i * 4 + 2];
                        rawData[i * 4 + 3] = tmpRawData[i * 4];
                    }
                    break;
                }
                default:
                    throw new ArgumentOutOfRangeException(nameof(Format), Format, null);
            }

            var bitmap = new Bitmap(ddtImage.Width, ddtImage.Height, PixelFormat.Format32bppArgb);

            var data = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height)
                , ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);

            unsafe
            {
                var p = (byte*) data.Scan0;
                for (var i = 0; i < bitmap.Width * bitmap.Height; i++)
                {
                    p[i * 4] = rawData[i * 4 + 2];
                    p[i * 4 + 1] = rawData[i * 4 + 1];
                    p[i * 4 + 2] = rawData[i * 4];
                    p[i * 4 + 3] = rawData[i * 4 + 3];
                }
            }

            bitmap.UnlockBits(data);
            return bitmap;
        }
    }

    public class DdtImage
    {
        public DdtImage(int width, int height, int offset, int length, byte[] rawData)
        {
            if (rawData == null)
                throw new ArgumentNullException(nameof(rawData));
            if (rawData.Length != length)
                throw new ArgumentOutOfRangeException(nameof(length), length, @"rawData.Length != length");
            Width = width;
            Height = height;
            Offset = offset;
            RawData = rawData;
        }

        public int Width { get; }
        public int Height { get; }
        public int Offset { get; }
        public int Length => RawData?.Length ?? 0;
        public byte[] RawData { get; }
    }
}