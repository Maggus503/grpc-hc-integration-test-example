using FluentAssertions;
using Grpc.Health.V1;
using static Grpc.Health.V1.Health;

namespace GrpcTestExample.IntegrationTests.Services
{
    public class HealthCheckServiceTests : IClassFixture<GrpcServiceFixture>
    {
        private readonly GrpcServiceFixture _serviceFixture;
        private readonly HealthClient _healthCheckService;

        public HealthCheckServiceTests(GrpcServiceFixture serviceFixture)
        {
            _serviceFixture = serviceFixture;
            _healthCheckService = new HealthClient(_serviceFixture.GrpcChannel);
        }

        [Fact(Skip = "This test may fail because current implementation of the health check service has significant startup delay.")]
        public async Task Check_ShouldReturnHealthyStatus_BecauseItIsHardcoded()
        {
            var result = await _healthCheckService.CheckAsync(new HealthCheckRequest());

            result.Status.Should().Be(HealthCheckResponse.Types.ServingStatus.Serving);
        }

        [Theory]
        [InlineData(5000)]
        public async Task Check_WithStartupDelay_ShouldReturnHealthyStatus_BecauseItIsHardcoded(int delay)
        {
            await Task.Delay(delay);

            var result = await _healthCheckService.CheckAsync(new HealthCheckRequest());

            result.Status.Should().Be(HealthCheckResponse.Types.ServingStatus.Serving);
        }
    }
}