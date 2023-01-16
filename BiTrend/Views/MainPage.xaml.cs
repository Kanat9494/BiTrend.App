namespace BiTrend.Views;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class MainPage : ContentPage
{
	public MainPage()
	{
		InitializeComponent();

		this.BindingContext = ViewModel = new MainViewModel();

        //ViewModel.OnAppearing();
    }

    MainViewModel ViewModel { get; }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        ViewModel.OnAppearing();
    }
}