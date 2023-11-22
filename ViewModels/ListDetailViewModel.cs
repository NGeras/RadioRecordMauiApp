namespace RadioRecord.ViewModels;

public partial class ListDetailViewModel : BaseViewModel
{

	[ObservableProperty]
	bool isRefreshing;

	[ObservableProperty]
	ObservableCollection<SampleItem> items;

    private readonly SampleDataService dataService;

    public ListDetailViewModel(SampleDataService service)
	{
		dataService = service;
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
		await dataService.GetItems();

		foreach (var item in items)
		{
			Items.Add(item);
		}
	}

	public async Task LoadDataAsync()
    {
        await dataService.GetItems();

        // Items = new ObservableCollection<SampleItem>(await dataService.GetItems());
    }

    [RelayCommand]
	private async Task GoToDetails(SampleItem item)
	{
		await Shell.Current.GoToAsync(nameof(ListDetailDetailPage), true, new Dictionary<string, object>
		{
			{ "Item", item }
		});
	}
}
