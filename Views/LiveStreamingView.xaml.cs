namespace RadioRecord.Views;

public partial class LiveStreamingView : ContentPage
{
    private readonly SampleDataService _dataService;

    public LiveStreamingView(SampleDataService dataService)
    {
        _dataService = dataService;
        InitializeComponent();
    }

    protected override async void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);
        var a = await _dataService.GetNowPlaying();
    }
}