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
            using (var fl = File.OpenWrite(file))
            {
                await fl.WriteAsync(bytes, 0, bytes.Length);
            }
        }

        public static async Task WriteToAsync(string file, Stream inputStream)
        {
            using (inputStream)
            {
                using (var fl = File.OpenWrite(file))
                {
                    await inputStream.WriteToAsync(fl);
                }
            }
        }

        public static async Task WriteAllTextAsync(string file, string text)
        {
            await WriteAllBytesAsync(file, Encoding.UTF8.GetBytes(text));
        }

        public static async Task WriteAllLinesAsync(string file, IEnumerable<string> lines)
        {
            
        }

        public static async Task<byte[]> ReadAllBytesAsync(string file)
        {
            using (var fl = File.OpenRead(file))
            {
                var len = fl.Length;
                var bytes = new byte[len];
                await fl.ReadAsync(bytes, 0, (int) len);
                return bytes;
            }
        }

        public static async Task<IEnumerable<string>> ReadAllLinesAsync(string file)
        {
            var lines = new List<string>();
            using (var fl = File.OpenRead(file))
            {
                using (var sr = new StreamReader(fl))
                {
                    while (!sr.EndOfStream)
                    {
                        lines.Add(await sr.ReadLineAsync());
                    }
                }
            }

            return lines;
        }

        public static async Task<string> ReadAllTextAsync(string file)
        {
            using (var fl = File.OpenRead(file))
            {
                var data = await fl.ReadToEndAsync();
                return data;
            }
        }

        public static Task<IEnumerable<string>> GetFileList(string directory)
        {
            return null;
//            Directory.file
        }
    }
}