
using System.Windows.Input;
using HealthcareManagementApp.Views;

namespace HealthcareManagementApp.ViewModels
{
    public class MainViewModel
    {
        public ICommand NavigateToPatientsCommand { get; }
        public ICommand NavigateToPhysiciansCommand { get; }
        public ICommand NavigateToAppointmentsCommand { get; }

        public MainViewModel()
        {
            NavigateToPatientsCommand = new Command(async () =>
            {
                await Shell.Current.GoToAsync(nameof(PatientsManagerView));
            });

            NavigateToPhysiciansCommand = new Command(async () =>
            {
                await Shell.Current.GoToAsync(nameof(PhysiciansManagerView));
            });

            NavigateToAppointmentsCommand = new Command(async () =>
            {
                await Shell.Current.GoToAsync(nameof(AppointmentsPageView));
            });
        }
    }
}
