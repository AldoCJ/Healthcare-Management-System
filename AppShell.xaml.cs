namespace HealthcareManagementApp
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            //Routing.RegisterRoute(nameof(MainPage), typeof(MainPage));
            Routing.RegisterRoute(nameof(Views.PatientsManagerView), typeof(Views.PatientsManagerView));
            Routing.RegisterRoute(nameof(Views.PhysiciansManagerView), typeof(Views.PhysiciansManagerView));
            Routing.RegisterRoute(nameof(Views.AppointmentsManagerView), typeof(Views.AppointmentsManagerView));
            Routing.RegisterRoute(nameof(Views.NewPatientView), typeof(Views.NewPatientView));
            Routing.RegisterRoute(nameof(Views.PhysicianView), typeof(Views.PhysicianView));

        }
    }
}
