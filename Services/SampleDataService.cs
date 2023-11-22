using Newtonsoft.Json.Linq;

namespace RadioRecord.Services;

public class SampleDataService
{
    private readonly HttpClient _httpClient;

    public SampleDataService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task GetItems()
    {
        var stations = await _httpClient.GetStringAsync("https://www.radiorecord.ru/api/stations/");
        var jstations = JsonSerializer.Deserialize<Root>(stations);

    }
}
