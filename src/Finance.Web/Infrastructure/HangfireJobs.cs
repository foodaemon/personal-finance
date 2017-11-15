using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;

namespace Finance.Web.Infrastructure
{
    public class HangfireJobs 
    {
        public async Task SendGetRequest()
        {
            var client = new HttpClient();
            await client.GetAsync("https://example.com");
        }

        public static void PrintToDebug(string message)
        {
            Debug.WriteLine(message);
        }
    }
}