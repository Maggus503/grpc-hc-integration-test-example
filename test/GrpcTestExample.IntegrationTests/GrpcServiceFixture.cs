using Grpc.Net.Client;
using GrpcService;
using Microsoft.AspNetCore.Mvc.Testing;

namespace GrpcTestExample.IntegrationTests
{
    public class GrpcServiceFixture : WebApplicationFactory<IGrpcServiceMarker>
    {
        public GrpcChannel GrpcChannel { get; set; }
        public HttpClient GrpcHttpClient { get; set; }

        public GrpcServiceFixture()
        {
            GrpcHttpClient = CreateClient();
            GrpcChannel = GrpcChannel.ForAddress(GrpcHttpClient.BaseAddress, new GrpcChannelOptions() { HttpClient = GrpcHttpClient });
        }
    }
}