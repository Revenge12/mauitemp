using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using $safeprojectname$.Services;
using $safeprojectname$.Views;
using Shared.Template.AuthModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace $safeprojectname$.ViewModel
{
    public partial class LoginViewModel : BaseViewModel
    {
        private readonly AuthService _authService;

        public LoginViewModel(AuthService authService)
        {
            _authService = authService;
        }

        [ObservableProperty]
        UserDto user = new();

        [ObservableProperty]
        bool showLoginError = false;
        

        [RelayCommand]
        public async Task Login()
        {
            var response = await _authService.Login(user);

            if(response.Success)
            {
                await SecureStorage.SetAsync("auth", response.Data);

                var claims = _authService.ParseClaimsFromJwt(response.Data);

                ShowLoginError = false;

                await Shell.Current.GoToAsync($"//{nameof(LoadingPage)}");
                
            }
            else
            {
                ShowLoginError = true;

            }

            
        }

    }
}
