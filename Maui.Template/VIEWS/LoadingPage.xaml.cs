using $safeprojectname$.Services;

namespace $safeprojectname$.Views;

public partial class LoadingPage : ContentPage
{
    private readonly AuthService _authService;

    public LoadingPage(AuthService authService)
	{
		InitializeComponent();
        _authService = authService;
    }

    protected override async void OnNavigatedTo(NavigatedToEventArgs args)
    {
        if (await _authService.GetAuthentication())
        {
            await Shell.Current.GoToAsync($"//{nameof(Home)}");
        }
        else
        {
            await Shell.Current.GoToAsync($"//{nameof(Login)}");
        }

    }
}