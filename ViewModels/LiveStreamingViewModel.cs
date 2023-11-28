namespace RadioRecord.ViewModels;

public partial class LiveStreamingViewModel : BaseViewModel
{
    private readonly SampleDataService _dataService;
    [ObservableProperty] private bool isRefreshing;
    [ObservableProperty] private ObservableCollection<ResultItem> items;


    public LiveStreamingViewModel(SampleDataService dataService)
    {
        _dataService = dataService;
    }

    public async Task LoadDataAsync()
    {
        var items = await _dataService.GetNowPlaying();
        Items = new ObservableCollection<ResultItem>(items);
    }

    [RelayCommand]
    private async Task GoToDetails(ResultItem item)
    {
        var station = item.Station;
        if (station == null) return;
        await Shell.Current.GoToAsync(nameof(ListDetailDetailPage), true, new Dictionary<string, object>
        {
            { "Item", station }
        });
    }

    [RelayCommand]
    private async Task OnRefreshing()
    {
        IsRefreshing = true;

        try
        {
            await LoadDataAsync();
        }
        finally
        {
            IsRefreshing = false;
        }
    }
}