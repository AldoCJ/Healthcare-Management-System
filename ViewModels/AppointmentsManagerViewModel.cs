using HealthcareManagementApp.Models;
using HealthcareManagementApp.Services;
using HealthcareManagementApp.Views;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace HealthcareManagementApp.ViewModels
{
    class AppointmentsManagerViewModel : INotifyPropertyChanged
    {
        private readonly AppointmentService appointmentService;
        private readonly PatientService patientService;
        private readonly PhysicianService physicianService;

        private string searchQuery = string.Empty;

        public ObservableCollection<Appointment> Appointments { get; set; }

        public ICommand NavigateBack { get; }
        public ICommand NavigateToNewAppointment { get; }
        public ICommand SearchCommand { get; }
        public ICommand DeleteCommand { get; }
        public ICommand EditCommand { get; }


        public AppointmentsManagerViewModel(AppointmentService _appointmentService, PatientService _patientService, PhysicianService _physicianService)
        {
            appointmentService = _appointmentService;
            patientService = _patientService;
            physicianService = _physicianService;

            Appointments = new ObservableCollection<Appointment>(appointmentService.GetAllAppointments());

            NavigateBack = new Command(async () =>
            {
                await Shell.Current.GoToAsync("///MainPage");
            });

            NavigateToNewAppointment = new Command(async () =>
            {
                await Shell.Current.GoToAsync(nameof(AppointmentView));
            });

            SearchCommand = new Command(Search);
            DeleteCommand = new Command<Appointment>(Delete);
            EditCommand = new Command<Appointment>(async (appointment) => await Edit(appointment));

        }

        public string SearchQuery
        {
            get => searchQuery;
            set
            {
                if (searchQuery != value)
                {
                    searchQuery = value;
                    NotifyPropertyChanged();
                }
            }

        }

        private void Delete(Appointment appointment)
        {
            if (appointment != null)
            {
                appointmentService.RemoveAppointment(appointment);
                Appointments.Remove(appointment);
            }
        }

        private async Task Edit(Appointment appointment)
        {
            if (appointment != null)
            {
                var parameters = new Dictionary<string, object>
                {
                    { "Appointment", appointment }
                };
                await Shell.Current.GoToAsync(nameof(AppointmentView), parameters);
            }
        }

        private void Search()
        {
            if (string.IsNullOrWhiteSpace(SearchQuery))
            {
                Refresh();
                return;
            }

            var results = appointmentService.GetPatientOrPhysicianAppointments(SearchQuery, SearchQuery);
            Appointments.Clear();
            foreach (var appt in results)
            {
                Appointments.Add(appt);
            }
        }

        public void Refresh()
        {
            Appointments.Clear();
            foreach (var appt in appointmentService.GetAllAppointments())
            {
                Appointments.Add(appt);
            }
        }   

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
