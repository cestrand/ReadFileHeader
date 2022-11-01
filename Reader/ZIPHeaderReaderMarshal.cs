using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ReadFileHeader.Reader;
public class ZIPHeaderReaderMarshal
{
    public static ZIPHeader? Read(FileStream fileStream)
    {
        BinaryReader reader = new BinaryReader(fileStream);

        int size = Marshal.SizeOf<ZIPHeader>();
        ReadOnlySpan<byte> span = new ReadOnlySpan<byte>(reader.ReadBytes(size));
        ZIPHeader header;
        bool succeed = MemoryMarshal.TryRead<ZIPHeader>(span, out header);
        return succeed ? header : null;
    }

    /// <summary>
    /// Header of basic PKZip file (without extensions).
    /// <see cref="https://www.fileformat.info/format/zip/corion.htm"/>
    /// <see cref="https://users.cs.jmu.edu/buchhofp/forensics/formats/pkzip.html"/>
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 2)]
    public unsafe struct ZIPHeader
    {
        public fixed byte id[4];
        public ushort version;
        public short generalPurposeBit;
        public short compressionMethod;
        public ushort modTime;
        public ushort modDate;
        public int crc1;
        public uint compressedFileSize;
        public uint uncompressedFileSize;
        public short lengthOfFilename;
        public short lengthOfExtraField;
    }
}

