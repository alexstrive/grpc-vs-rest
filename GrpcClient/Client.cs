using System;
using System.Net.Http;
using System.Threading.Tasks;
using Grpc.Core;
using Grpc.Net.Client;
using GrpcServer;

namespace GrpcClient
{
    class Client
    {
        static async Task Main(string[] args)
        {
            // Display the number of command line arguments.
            using var channel = GrpcChannel.ForAddress("http://127.0.0.1:5000");
            var client =  new CovidStats.CovidStatsClient(channel);
            // var reply = client.GetAll(new EmptyResponse());
            //
            //
            // await foreach (var response in reply.ResponseStream.ReadAllAsync())
            // {
            //     Console.WriteLine("Greeting: " + response);
            //     // "Greeting: Hello World" is written multiple times
            // }
            var responses = client.GetAllAsList(new EmptyResponse());
            Console.WriteLine(responses.Entries.Count);
        }
    }
}