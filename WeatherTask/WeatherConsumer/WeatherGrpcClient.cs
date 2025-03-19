using Grpc.Core;
using Grpc.Net.Client;
using WeatherService.Protos;

namespace WeatherConsumer;

public class WeatherGrpcClient : IDisposable
{
    private readonly GrpcChannel _channel;
    private readonly Weather.WeatherClient _client;

    public WeatherGrpcClient(string grpcUrl)
    {
        var channelOptions = new GrpcChannelOptions
        {
            HttpHandler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
            },
            UnsafeUseInsecureChannelCallCredentials = true
        };

        _channel = GrpcChannel.ForAddress(grpcUrl, channelOptions);
        _client = new Weather.WeatherClient(_channel);
    }

    public async Task SetWeatherAsync(double temp, string description)
    {
        try
        {
            var request = new SetWeatherRequest
            {
                Temp = temp,
                Description = description
            };
            await _client.SetWeatherAsync(request);
        }
        catch (RpcException ex)
        {
            Console.WriteLine($"gRPC call failed: {ex.Status.Detail}");
            throw;
        }
    }

    public void Dispose()
    {
        _channel?.Dispose();
    }
}