using System.Text;

namespace RadioRecord.Services;

public class SampleDataService
{
    private const string StationsUrl = "https://www.radiorecord.ru/api/stations/";
    private const string HistoryUrl = "https://www.radiorecord.ru/api/station/history?id=";
    private const string NowPlayingUrl = "https://www.radiorecord.ru/api/stations/now/";

    private const string defaultTrackImageUrl =
        "https://www.radiorecord.ru/local/templates/record/assets/build/images/DefaultTrack_600.png";

    private readonly HttpClient _httpClient;

    public SampleDataService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public List<Station> Stations { get; private set; }

    public async Task<Root> GetStations()
    {
        var stations = await _httpClient.GetStringAsync(StationsUrl);
        var root = await JsonSerializer.DeserializeAsync<Root>(new MemoryStream(Encoding.UTF8.GetBytes(stations)));
        Stations = root?.result?.stations;
        return root;
    }

    public async Task<List<History>> GetHistory(int id, CancellationToken token)
    {
        var historyRoot = await _httpClient.GetStringAsync($"{HistoryUrl}{id}", token);
        var resultHistoryRoot =
            await JsonSerializer.DeserializeAsync<Root>(new MemoryStream(Encoding.UTF8.GetBytes(historyRoot)));
        var resultHistory = resultHistoryRoot.result?.history;
        if (resultHistory != null)
            foreach (var history in resultHistory.Where(history => string.IsNullOrWhiteSpace(history.listenUrl)))
                history.image100 = defaultTrackImageUrl;
        return resultHistory;
    }

    public async Task<List<ResultItem>> GetNowPlaying()
    {
        var nowPlayingRootJson = await _httpClient.GetStringAsync(NowPlayingUrl);
        var nowPlayingRoot =
            await JsonSerializer.DeserializeAsync<LiveRoot>(
                new MemoryStream(Encoding.UTF8.GetBytes(nowPlayingRootJson)));
        var nowPlaying = nowPlayingRoot?.result;
        if (nowPlaying != null)
            foreach (var item in nowPlaying)
            {
                if (string.IsNullOrWhiteSpace(item.track.listenUrl)) item.track.image600 = defaultTrackImageUrl;
                item.Station = Stations.FirstOrDefault(x => x.id.Equals(item.id));
            }

        return nowPlaying;
    }
}