using CommunityToolkit.Maui.Core.Primitives;
using CommunityToolkit.Maui.Views;

namespace RadioRecord.Views;

public partial class ListDetailDetailPage : ContentPage
{
    private readonly ListDetailDetailViewModel ViewModel;

    public ListDetailDetailPage(ListDetailDetailViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = ViewModel = viewModel;
        mediaElement.StateChanged += MediaElementOnStateChanged;
        ViewModel.NowPlaying.PropertyChanged += NowPlayingOnPropertyChanged;
    }

    private void NowPlayingOnPropertyChanged(object sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName is nameof(ViewModel.NowPlaying.Item))
            if (ViewModel.NowPlaying.Item == null)
                mediaElement.Stop();
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
        mediaElement.Source = MediaSource.FromUri(ViewModel.Item.stream_hls);
        mediaElement.Play();
    }
}