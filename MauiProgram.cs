namespace RadioRecord;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.UseMauiCommunityToolkitMediaElement()
			.UseMauiCommunityToolkit()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});
        builder.Services.AddSingleton<HttpClient>();

        builder.Services.AddSingleton<SampleDataService>();

		builder.Services.AddSingleton<NowPlayingViewModel>();
		builder.Services.AddSingleton<ListDetailDetailPage>();
        builder.Services.AddSingleton<ListDetailDetailViewModel>();

        builder.Services.AddSingleton<ListDetailViewModel>();

		builder.Services.AddSingleton<ListDetailPage>();


		builder.Services.AddSingleton<LocalizationViewModel>();

		builder.Services.AddSingleton<LocalizationPage>();
		builder.Services.AddSingleton<LiveStreamingView>();

		return builder.Build();
	}
}
