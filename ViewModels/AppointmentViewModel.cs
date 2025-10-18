using HealthcareManagementApp.Models;
using HealthcareManagementApp.Services;
using HealthcareManagementApp.Views;
using System.ComponentModel;
using System.Windows.Input;

namespace HealthcareManagementApp.ViewModels
{
    [QueryProperty(nameof(SelectedAppointment), "Appointment")]
    public class AppointmentViewModel : INotifyPropertyChanged
    {
        private readonly AppointmentService appointmentService;
        private readonly PatientService patientService;
        private readonly PhysicianService physicianService;

        private Appointment? selectedAppointment;

        public ICommand NavigateBack { get; }
        public ICommand SaveCommand { get; }

        private DateTime date = DateTime.Today;
        private TimeSpan startTime;
        private TimeSpan endTime;
        public List<Patient> Patients => patientService.GetAllPatients().ToList();
        public List<Physician> Physicians => physicianService.GetAllPhysicians().ToList();
        private Patient? selectedPatient;
        private Physician? selectedPhysician;

        public AppointmentViewModel(AppointmentService _appointmentService, PatientService _patientService, PhysicianService _physicianService, Appointment? appointment = null)
        {
            appointmentService = _appointmentService;
            patientService = _patientService;
            physicianService = _physicianService;
            selectedAppointment = appointment;

            NavigateBack = new Command(async () =>
            {
                await Shell.Current.GoToAsync("..");
            });

            SaveCommand = new Command(Save);

            StartTime = new TimeSpan(9, 0, 0);
            EndTime = new TimeSpan(17, 0, 0);
        }

        public Appointment? SelectedAppointment
        {
            get => selectedAppointment;
            set
            {
                selectedAppointment = value;
                if (selectedAppointment != null)
                {
                    Date = selectedAppointment.StartTime.Date;
                    StartTime = selectedAppointment.StartTime.TimeOfDay;
                    EndTime = selectedAppointment.EndTime.TimeOfDay;
                    SelectedPatient = selectedAppointment.Patient;
                    SelectedPhysician = selectedAppointment.Physician;
                }
            }
        }

        public DateTime Date
        {
            get => date;
            set
            {                                           
                date = value; NotifyPropertyChanged();
            }
        }

        public TimeSpan StartTime
        {
            get => startTime;
            set
            {                                           
                startTime = value; NotifyPropertyChanged();
            }
        }

        public TimeSpan EndTime
        {
            get => endTime;
            set
            {                                           
                endTime = value; NotifyPropertyChanged();
            }
        }

        public Patient? SelectedPatient
        {
            get => selectedPatient;
            set
            {                                           
                selectedPatient = value; NotifyPropertyChanged();
            }
        }

        public Physician? SelectedPhysician
        {
            get => selectedPhysician;
            set
            {                                           
                selectedPhysician = value; NotifyPropertyChanged();
            }
        }

        private async void Save()
        {
            try
            {
                if (SelectedAppointment == null)
                {
                    if (SelectedPatient == null || SelectedPhysician == null)
                    {
                        throw new ArgumentException("Patient and Physician must be selected.");
                    }

                    var startDateTime = Date.Date + StartTime;
                    var endDateTime = Date.Date + EndTime;

                    var newAppointment = new Appointment(startDateTime, endDateTime, SelectedPatient, SelectedPhysician);
                    appointmentService.AddAppointment(newAppointment);
                }
                else
                {
                    var newStart = Date.Date + StartTime;
                    var newEnd = Date.Date + EndTime;
                    appointmentService.RescheduleAppointment(SelectedAppointment, newStart, newEnd);
                }
                await Shell.Current.GoToAsync("..");
            }
            catch (Exception ex)
            {
                if (Application.Current?.MainPage != null)
                {
                    await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
                }
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        private void NotifyPropertyChanged([System.Runtime.CompilerServices.CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
