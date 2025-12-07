using System.IO.Compression;
using System.Text;

namespace Api.Bootstrap
{
    public class Strings
    {
        public static string Zip(string text)
        {
            var bytes = Encoding.UTF8.GetBytes(text);

            using var output = new MemoryStream();
            using (var gzip = new GZipStream(output, CompressionLevel.Optimal))
            {
                gzip.Write(bytes, 0, bytes.Length);
            }

            return Convert.ToBase64String(output.ToArray());
        }

        public static string Unzip(string base64Gzip)
        {
            var gzipBytes = Convert.FromBase64String(base64Gzip);

            using var input = new MemoryStream(gzipBytes);
            using var gzip = new GZipStream(input, CompressionMode.Decompress);
            using var output = new MemoryStream();

            gzip.CopyTo(output);

            return Encoding.UTF8.GetString(output.ToArray());
        }
    }
}
