namespace RadioRecord.Views;

public partial class LiveStreamingView : ContentPage
{
    private readonly LiveStreamingViewModel ViewModel;

    public LiveStreamingView(LiveStreamingViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = ViewModel = viewModel;
    }

    protected override async void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);
        await ViewModel.LoadDataAsync();
    }
}