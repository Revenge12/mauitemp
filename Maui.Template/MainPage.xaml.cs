using $safeprojectname$.Services;
using $safeprojectname$.Views;

namespace $safeprojectname$
{
    public partial class MainPage : ContentPage
    {


        public MainPage()
        {
            InitializeComponent();
        }

        protected override async void OnNavigatedTo(NavigatedToEventArgs args)
        {
            await Shell.Current.GoToAsync($"{nameof(LoadingPage)}");

            
            


        }

    }

}
