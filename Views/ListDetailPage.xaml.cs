namespace RadioRecord.Views;

public partial class ListDetailPage : ContentPage
{
    private readonly ListDetailViewModel ViewModel;

    public ListDetailPage(ListDetailViewModel viewModel)
    {
        InitializeComponent();
        HandlerChanged += OnHandlerChanged;

        BindingContext = ViewModel = viewModel;
        Task.Run(() => ViewModel.LoadDataAsync());
    }
    void OnHandlerChanged(object sender, EventArgs e)
    {
        var mediaService = Handler.MauiContext.Services.GetRequiredService<MediaService>();
        mediaService.MediaElement?.Handler?.DisconnectHandler();
        mediaService.MediaElement?.Dispose();
        mediaService.EnsureMediaElementExists();
        NowPlayingGrid.Add(mediaService.MediaElement);
    }
}