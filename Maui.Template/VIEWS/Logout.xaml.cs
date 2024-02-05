namespace $safeprojectname$.Views;

public partial class Logout : ContentPage
{
	public Logout()
	{
		InitializeComponent();
	}

    protected override async void OnNavigatedTo(NavigatedToEventArgs args)
    {
        SecureStorage.RemoveAll();
        await Shell.Current.GoToAsync($"//{nameof(LoadingPage)}");
    }
}