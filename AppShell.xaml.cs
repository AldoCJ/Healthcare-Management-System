namespace HealthcareManagementApp
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            //Routing.RegisterRoute(nameof(MainPage), typeof(MainPage));
            Routing.RegisterRoute(nameof(Views.PatientsManagerView), typeof(Views.PatientsManagerView));
            Routing.RegisterRoute(nameof(Views.PhysiciansPageView), typeof(Views.PhysiciansPageView));
            Routing.RegisterRoute(nameof(Views.AppointmentsPageView), typeof(Views.AppointmentsPageView));
            Routing.RegisterRoute(nameof(Views.NewPatientView), typeof(Views.NewPatientView));

        }
    }
}
