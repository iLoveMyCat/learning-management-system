namespace eTeacher.Services
{
    public class MockAwsS3Reader : IAwsS3Reader
    {
        // Hard-coded ofcourse we will have it in vault or appsettings
        private const string SeedKey = "string"; // used string just to easily test with swagger
        private const string SeedJson = @"[
  { ""FirstName"": ""Ada"",  ""LastName"": ""Lovelace"", ""Email"": ""ada@example.com"" },
  { ""FirstName"": ""Alan"", ""LastName"": ""Turing"",   ""Email"": ""alan@example.com"" }
]";

        public Task<string> DownloadTextAsync(string key, CancellationToken ct = default)
        {
            if (key != SeedKey)
                throw new FileNotFoundException($"Mock key not found: {key}. Try '{SeedKey}'.");

            return Task.FromResult(SeedJson);
        }
    }
}
