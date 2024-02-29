using Grpc.Core;
using Grpc.Net.Client;
using GrpcServer;
using GrpcServer.Protos;
internal class Program
{
    private static async Task Main(string[] args)
    {
        var channel = GrpcChannel.ForAddress("http://localhost:5137");


        var authenticationClient = new Authentication.AuthenticationClient(channel);
        var authenticationResponse = authenticationClient.Authenticate(new AuthenticationRequest
        {
            UserName = "admin",
            Password = "admin"
        });
        if (authenticationResponse.AccessToken.Length >= 1)
        {
            Console.WriteLine("usuario logado com sucesso");
            var headers = new Metadata { { "Authorization", $"Bearer {authenticationResponse.AccessToken}" } };

            var greeterclient = new Greeter.GreeterClient(channel);
            var reply = await greeterclient.SayHelloAsync(new HelloRequest { Name = "lindinho da mamae" }, headers);
            Console.WriteLine(reply.Message);
        }
        else
        {
            Console.WriteLine("usuario nao autorizado");
            Console.ReadLine(); return;
        }
        await channel.ShutdownAsync();
        Console.WriteLine("GFDGs");
        Console.ReadLine();
    }




}