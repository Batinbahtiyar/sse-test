using System;
using System.IO;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;


namespace sse_test
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Press enter to start!");
            Console.ReadLine();

            var sseClient = new SseServiceClient();
            
            Task
                .Run(sseClient.ReadSseServiceData)
                .Wait();

        }

       
    }
}
