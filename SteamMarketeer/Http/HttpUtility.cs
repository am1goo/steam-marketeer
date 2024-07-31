using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

public static class HttpUtility
{
    public static async Task<string> GetContentAsync(Uri uri, CancellationToken cancellationToken)
    {
        using (var client = new HttpClient())
        {
            var resp = await client.GetAsync(uri, cancellationToken: cancellationToken);
            var pageContent = await resp.Content.ReadAsStringAsync();
            return pageContent;
        }
    }
}
