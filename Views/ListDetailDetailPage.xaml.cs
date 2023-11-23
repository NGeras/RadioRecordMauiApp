namespace RadioRecord.Views;

public partial class ListDetailDetailPage : ContentPage
{
    private readonly ListDetailDetailViewModel ViewModel;

    public ListDetailDetailPage(ListDetailDetailViewModel viewModel)
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