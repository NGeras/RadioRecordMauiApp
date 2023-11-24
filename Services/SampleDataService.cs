using System.Text;

namespace RadioRecord.Services;
public class SampleDataService
{
    private readonly HttpClient _httpClient;
    private const string StationsUrl = "https://www.radiorecord.ru/api/stations/";
    private const string HistoryUrl = "https://www.radiorecord.ru/api/station/history?id=";
    private const string NowPlayingUrl = "https://www.radiorecord.ru/api/stations/now/";
    public SampleDataService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    public List<Station> Stations { get; private set; }
    public async Task<Root> GetStations()
    {
        try
        {
            var stations = await _httpClient.GetStringAsync(StationsUrl);
            var root = await JsonSerializer.DeserializeAsync<Root>(new MemoryStream(Encoding.UTF8.GetBytes(stations)));
            Stations = root?.result?.stations;
            return root;
        }
        catch (Exception ex)
        {
            // Handle or log exception
            throw;
        }
    }
    public async Task<Root> GetHistory(int id, CancellationToken token)
    {
        try
        {
            var history = await _httpClient.GetStringAsync($"{HistoryUrl}{id}", token);
            return await JsonSerializer.DeserializeAsync<Root>(new MemoryStream(Encoding.UTF8.GetBytes(history)));
        }
        catch (Exception ex)
        {
            // Handle or log exception
            throw;
        }
    }
    public async Task<List<ResultItem>> GetNowPlaying()
    {
        try
        {
            var nowPlayingRootJson = await _httpClient.GetStringAsync(NowPlayingUrl);
            var nowPlayingRoot = await JsonSerializer.DeserializeAsync<LiveRoot>(new MemoryStream(Encoding.UTF8.GetBytes(nowPlayingRootJson)));
            var nowPlaying = nowPlayingRoot?.result;
            if (nowPlaying != null)
            {
                foreach (var item in nowPlaying) item.Station = Stations.FirstOrDefault(x => x.id.Equals(item.id));
            }
            return nowPlaying;
        }
        catch (Exception ex)
        {
            // Handle or log exception
            throw;
        }
    }
}