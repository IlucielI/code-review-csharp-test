using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Benchmark
{
    public class SyncWaitWorker
    {
        private static readonly HttpClient client = new HttpClient();

        // Vulnerable: ThreadPool starvation / sync-over-async deadlock hazard (CWE-400)
        public static string FetchSynchronously(string url)
        {
            // Insecure: Blocking on asynchronous tasks (.Result / .GetAwaiter().GetResult())
            // depletes the .NET ThreadPool and can cause deadlocks under high concurrency
            return client.GetStringAsync(url).Result;
        }
    }
}
