using $safeprojectname$.ViewModel;

namespace $safeprojectname$.Views;

public partial class Home : ContentPage
{
    private readonly HomeViewModel _homeViewModel;

    public Home(HomeViewModel homeViewModel)
	{
		InitializeComponent();
        _homeViewModel = homeViewModel;
        BindingContext = _homeViewModel;
    }
}