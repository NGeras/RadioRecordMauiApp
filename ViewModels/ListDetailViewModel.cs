namespace RadioRecord.ViewModels;

public partial class ListDetailViewModel : BaseViewModel
{
    private readonly SampleDataService dataService;

    [ObservableProperty] private bool isRefreshing;

    [ObservableProperty] private ObservableCollection<Station> items;
    [ObservableProperty] private ObservableCollection<Genre> genres;
    [ObservableProperty] private NowPlayingViewModel nowPlaying;
    [ObservableProperty] private int span;
    [ObservableProperty] private string selectedGenre;

    public ListDetailViewModel(SampleDataService service, NowPlayingViewModel nowPlayingViewModel)
    {
        dataService = service;
        nowPlaying = nowPlayingViewModel;
        if (DeviceInfo.Idiom == DeviceIdiom.Desktop || DeviceInfo.Idiom == DeviceIdiom.Tablet ||
            DeviceInfo.Idiom == DeviceIdiom.TV)
        {
            Span = 2;
        }
        else
        {
            Span = 1;
            DeviceDisplay.MainDisplayInfoChanged += DeviceDisplayOnMainDisplayInfoChanged;
        }
    }

    private void DeviceDisplayOnMainDisplayInfoChanged(object sender, DisplayInfoChangedEventArgs e)
    {
        SetSpanAccordingToScreenSize();
    }

    private void SetSpanAccordingToScreenSize()
    {
        Span = DeviceDisplay.MainDisplayInfo.Width >= 1000 ? 2 : 1; // Adjust the threshold as needed
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

    [RelayCommand]
    public void StopPlaying()
    {
        NowPlaying.Item = null;
    }

    [RelayCommand]
    private async Task TextChanged(string newText)
    {
        if (string.IsNullOrEmpty(newText)) Items = new ObservableCollection<Station>(dataService.Stations);
    }

    [RelayCommand]
    public async Task PerformSearch(string query)
    {
        var sortedList = dataService.Stations
            .Where(station => station.title.IndexOf(query, StringComparison.OrdinalIgnoreCase) >= 0 ||
                              station.genre.Any(g =>
                                  g.name.ToString().IndexOf(query, StringComparison.OrdinalIgnoreCase) >= 0)).ToList();
        Items = new ObservableCollection<Station>(sortedList);
    }

    [RelayCommand]
    private void AddToFavorites(Station station)
    {
        // Favorites.AddToFavorites(station);
        // Any other logic or UI updates after adding to favorites
    }

    public async Task LoadDataAsync()
    {
        var roots = await dataService.GetStations();
        Items = new ObservableCollection<Station>(roots.result.stations);
        Genres = new ObservableCollection<Genre>(roots.result.genre);
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