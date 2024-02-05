using $safeprojectname$.Views;

namespace $safeprojectname$
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();


            Routing.RegisterRoute(nameof(LoadingPage), typeof(LoadingPage));
            Routing.RegisterRoute(nameof(Home), typeof(Home));
            Routing.RegisterRoute(nameof(MainPage), typeof(MainPage));
            Routing.RegisterRoute(nameof(Login), typeof(Login));
            Routing.RegisterRoute(nameof(Logout), typeof(Logout));


        }


    }
}
