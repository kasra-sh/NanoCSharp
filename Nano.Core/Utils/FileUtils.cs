using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using ServiceStack;

namespace Nano.Core.Utils
{
    public class FileUtil
    {
        public static async Task WriteAllBytesAsync(string file, byte[] bytes)
        {
            var fl = File.OpenWrite(file);
            await fl.WriteAsync(bytes, 0, bytes.Length);
            fl.Close();
        }

        public static async Task WriteToAsync(string file, Stream inputStream)
        {
            var fl = File.OpenWrite(file);
            await inputStream.WriteToAsync(fl);
            inputStream.Close();
            fl.Close();
        }

        public static async Task WriteAllTextAsync(string file, string text)
        {
            await WriteAllBytesAsync(file, Encoding.UTF8.GetBytes(text));
        }

        public static async Task<string> ReadAllTextAsync(string file)
        {
            return await File.OpenRead(file).ReadToEndAsync();
        }

        public static Task<IEnumerable<string>> GetFileList(string directory)
        {
            return null;
//            Directory.file
        }
    }
}
