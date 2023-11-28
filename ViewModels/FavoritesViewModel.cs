namespace RadioRecord.ViewModels;

public partial class FavoritesViewModel : BaseViewModel
{
    private readonly SampleDataService _dataService;
    [ObservableProperty] private ObservableCollection<Station> _favoriteStations;

    public FavoritesViewModel(SampleDataService dataService)
    {
        _dataService = dataService;
    }
    public async Task LoadFavoriteStations()
    {
        await _dataService.GetStations();
        // Load favorite stations from local storage
        var get = await SecureStorage.GetAsync("FavoriteStations");
        FavoriteStations = new ObservableCollection<Station>(
            get.Split(',')?
                .Select(id => _dataService.Stations.FirstOrDefault(x => x.id== int.Parse(id))) ?? Array.Empty<Station>());
    }
    public void AddToFavorites(Station station)
    {
        FavoriteStations ??= new ObservableCollection<Station>();

        if (FavoriteStations.All(s => s.id != station.id))
        {
            FavoriteStations.Add(station);
            SaveFavoriteStations();
        }
    }

    private void SaveFavoriteStations()
    {
        // Save favorite stations to local storage
        SecureStorage.SetAsync("FavoriteStations", string.Join(',', FavoriteStations.Select(s => s.id.ToString())));
    }
}