using System.Text;
using System.Runtime.InteropServices;
using ReadFileHeader.Reader;

namespace ReadFileHeader
{
    internal class Program
    {
        static unsafe void Main(string[] args)
        {
            FileStream file = File.OpenRead("sample.zip");
            var header = ZIPHeaderReaderMarshal.Read(file);

            Console.WriteLine($"Size of uncompressed file: {header?.uncompressedFileSize} bytes");
        }
    }
}