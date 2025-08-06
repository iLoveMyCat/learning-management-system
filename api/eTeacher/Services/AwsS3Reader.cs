using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Amazon.S3;
using eTeacher.Services;

namespace eTeacher.Services
{
    public class AwsS3Reader : IAwsS3Reader
    {
        private readonly IAmazonS3 _s3;
        private readonly string _bucket;

        public AwsS3Reader(IAmazonS3 s3, IConfiguration cfg)
        {
            _s3 = s3;
            _bucket = cfg["S3:BucketName"] ?? "eteacher-demo-bucket";
        }

        public async Task<string> DownloadTextAsync(string key, CancellationToken ct = default)
        {
            var resp = await _s3.GetObjectAsync(_bucket, key, ct);
            using var reader = new StreamReader(resp.ResponseStream, Encoding.UTF8);
            return await reader.ReadToEndAsync();
        }
    }
}
