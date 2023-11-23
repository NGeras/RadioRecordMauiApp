namespace RadioRecord.ViewModels;

public partial class ListDetailViewModel : BaseViewModel
{
    private readonly SampleDataService dataService;

    [ObservableProperty] private ListDetailDetailViewModel _detailViewModel;

    [ObservableProperty] private bool isRefreshing;

    [ObservableProperty] private ObservableCollection<Station> items;

    public ListDetailViewModel(SampleDataService service, ListDetailDetailViewModel detailViewModel)
    {
        dataService = service;
        _detailViewModel = detailViewModel;
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

    [RelayCommand]
    public async Task LoadMore()
    {
        var roots = await dataService.GetStations();

        foreach (var item in items) Items.Add(item);
    }

    public async Task LoadDataAsync()
    {
        var roots = await dataService.GetStations();
        Items = new ObservableCollection<Station>(roots.result.stations);
    }

    [RelayCommand]
    private async Task GoToDetails(Station item)
    {
        await Shell.Current.GoToAsync(nameof(ListDetailDetailPage), true, new Dictionary<string, object>
        {
            { "Item", item }
        });
    }
}