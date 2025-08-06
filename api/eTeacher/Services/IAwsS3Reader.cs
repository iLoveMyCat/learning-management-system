namespace eTeacher.Services
{
    public interface IAwsS3Reader
    {
        Task<string> DownloadTextAsync(string key, CancellationToken ct = default);
    }
}
