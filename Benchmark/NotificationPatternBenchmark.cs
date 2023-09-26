using BenchmarkDotNet.Attributes;
using Domain.Entities;
using System.Net.Http.Json;

namespace Benchmark;

[MemoryDiagnoser]
[RankColumn]
public sealed class NotificationPatternBenchmark
{
    private HttpClient _httpClient;

    [GlobalSetup]
    public void GlobalSetup()
    {
        _httpClient = new HttpClient();
    }

    [Benchmark]
    public async Task HttpClientGetRequest()
    {
        const string addPersonRequestUri = @"https://localhost:1001/api/Person/add-person";
        var person = new Person()
        {
            Name = "random"
        };

        HttpResponseMessage response = await _httpClient.PostAsJsonAsync(addPersonRequestUri, person);
        Console.WriteLine(response.StatusCode);
        Console.WriteLine(await response.Content.ReadAsStringAsync());

    }
}
