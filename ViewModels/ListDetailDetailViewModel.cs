namespace RadioRecord.ViewModels;

[QueryProperty(nameof(Item), "Item")]
public partial class ListDetailDetailViewModel : BaseViewModel
{
    private const string defaultTrackImageUrl =
        "https://www.radiorecord.ru/local/templates/record/assets/build/images/DefaultTrack_600.png";

    private readonly SampleDataService _dataService;
    private CancellationTokenSource _tokenSource;

    [ObservableProperty] private ObservableCollection<History> historyItems;

    [ObservableProperty] private Station item;
    [ObservableProperty] private NowPlayingViewModel nowPlaying;

    public ListDetailDetailViewModel(SampleDataService dataService, NowPlayingViewModel nowPlayingViewModel)
    {
        _dataService = dataService;
        nowPlaying = nowPlayingViewModel;
        _tokenSource = new CancellationTokenSource();
    }

    public async Task LoadDataAsync()
    {
        try
        {
            var historyRoot = await _dataService.GetHistory(Item.id, _tokenSource.Token);
            var histories = historyRoot.result.history;
            if (histories.Count < 1)
            {
                HistoryItems = new ObservableCollection<History>();
                return;
            }

            var resultHistory = histories.Where(x => !string.IsNullOrWhiteSpace(x.song)).Take(10);
            foreach (var history in resultHistory)
                if (string.IsNullOrWhiteSpace(history.listenUrl))
                    history.image100 = defaultTrackImageUrl;
            HistoryItems = new ObservableCollection<History>(resultHistory);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }

    public void Cancel()
    {
        _tokenSource.Cancel();
        _tokenSource = new CancellationTokenSource();
    }

    [RelayCommand]
    private async Task OpenUri(History item)
    {
        await Launcher.OpenAsync(item.shareUrl);
    }
}