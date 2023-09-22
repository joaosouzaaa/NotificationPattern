namespace NotificationPatternIntegrationTests.Fixture;
public abstract class HttpClientFixture : IClassFixture<ContainerFixture>
{
	private readonly ContainerFixture _containerFixture;
    protected readonly HttpClient _httpClient;

	public HttpClientFixture(ContainerFixture containerFixture)
	{
		_containerFixture = containerFixture;
		_httpClient = _containerFixture.CreateClient();
	}
}
