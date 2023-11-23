namespace RadioRecord.ViewModels;

[QueryProperty(nameof(Item), "Item")]
public partial class ListDetailDetailViewModel : BaseViewModel
{
    private readonly SampleDataService _dataService;

    [ObservableProperty] private ObservableCollection<History> historyItems;

    [ObservableProperty] private Station item;

    public ListDetailDetailViewModel(SampleDataService dataService)
    {
        _dataService = dataService;
    }

    public async Task LoadDataAsync()
    {
        try
        {
            var historyRoot = await _dataService.GetHistory(Item.id);
            var resultHistory = historyRoot.result.history.Where(x => !string.IsNullOrWhiteSpace(x.song)).Take(10);
            foreach (var history in resultHistory)
                if (string.IsNullOrWhiteSpace(history.listenUrl))
                    history.image100 =
                        "https://www.radiorecord.ru/local/templates/record/assets/build/images/DefaultTrack_600.png";

            HistoryItems = new ObservableCollection<History>(resultHistory);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }

    [RelayCommand]
    private async Task OpenUri(History item)
    {
        await Launcher.OpenAsync(item.shareUrl);
    }
}