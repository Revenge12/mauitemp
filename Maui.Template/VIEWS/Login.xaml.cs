using $safeprojectname$.ViewModel;

namespace $safeprojectname$.Views;

public partial class Login : ContentPage
{
    private readonly LoginViewModel _viewmodel;

    public Login(LoginViewModel viewmodel)
	{
		InitializeComponent();
        _viewmodel = viewmodel;
        BindingContext = _viewmodel;
    }


}