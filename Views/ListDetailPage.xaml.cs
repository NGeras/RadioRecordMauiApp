namespace RadioRecord.Views;

public partial class ListDetailPage : ContentPage
{
    private readonly ListDetailViewModel ViewModel;

    public ListDetailPage(ListDetailViewModel viewModel)
    {
        InitializeComponent();

        BindingContext = ViewModel = viewModel;
        ViewModel.LoadDataAsync();
    }
}