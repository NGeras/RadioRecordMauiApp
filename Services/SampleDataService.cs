namespace RadioRecord.Services;

public class SampleDataService
{
    private readonly HttpClient _httpClient;

    public SampleDataService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<Root> GetStations()
    {
        var stations = await _httpClient.GetStringAsync("https://www.radiorecord.ru/api/stations/");
        return JsonSerializer.Deserialize<Root>(stations);
    }

    public async Task<Root> GetHistory(int id)
    {
        var stations = await _httpClient.GetStringAsync($"https://www.radiorecord.ru/api/station/history?id={id}");
        return JsonSerializer.Deserialize<Root>(stations);
    }

    public async Task<Root> GetNowPlaying()
    {
        throw new NotImplementedException();
    }
}