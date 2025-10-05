namespace HealthcareManagementApp
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(MainPage), typeof(MainPage));
            Routing.RegisterRoute(nameof(Views.PatientsPageView), typeof(Views.PatientsPageView));
            Routing.RegisterRoute(nameof(Views.PhysiciansPageView), typeof(Views.PhysiciansPageView));
            Routing.RegisterRoute(nameof(Views.AppointmentsPageView), typeof(Views.AppointmentsPageView));

        }
    }
}
