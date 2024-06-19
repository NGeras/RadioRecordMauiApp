using CommunityToolkit.Maui.Core.Primitives;
using CommunityToolkit.Maui.Views;

namespace RadioRecord.Views;

public partial class ListDetailDetailPage : ContentPage
{
    private readonly ListDetailDetailViewModel ViewModel;
    private readonly MediaService _mediaService;

    public ListDetailDetailPage(ListDetailDetailViewModel viewModel, MediaService mediaService)
    {
        InitializeComponent();
        BindingContext = ViewModel = viewModel;
        _mediaService = mediaService;
        _mediaService.StateChanged += MediaElementOnStateChanged;
    }

    private void MediaElementOnStateChanged(object sender, MediaStateChangedEventArgs e)
    {
        if (e.NewState == MediaElementState.Playing) ViewModel.NowPlaying.Item = ViewModel.Item;
    }

    protected override async void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);
        await ViewModel.LoadDataAsync();
    }

    protected override void OnNavigatingFrom(NavigatingFromEventArgs args)
    {
        base.OnNavigatingFrom(args);
        ViewModel.Cancel();
    }

    private void PlayButton_OnClicked(object sender, EventArgs e)
    {
        //mediaElement.Source = MediaSource.FromUri(ViewModel.Item.stream_hls);
        //mediaElement.MetadataTitle = ViewModel.Item.title;
        //mediaElement.MetadataArtist = "RadioRecord";
        //mediaElement.MetadataArtworkUrl = "http://www.myownpersonaldomain.com/image.jpg";
        _mediaService.Play(ViewModel.Item);
    }
}