using HealthcareManagementApp.Models;
using HealthcareManagementApp.Services;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using System.Timers;

namespace HealthcareManagementApp.ViewModels
{
    public class PatientsManagerViewModel : INotifyPropertyChanged
    {
        private readonly PatientService patientService;

        private string searchQuery = string.Empty;
        private Patient? selectedPatient;
        public ObservableCollection<Patient> Patients { get; set; }
        public ICommand NavigateBack { get; }
        public ICommand SearchCommand { get; }
        public ICommand DeleteCommand { get; }

        private System.Timers.Timer searchDebounceTimer;
        private const int DebounceDelay = 500; // ms

        public PatientsManagerViewModel(PatientService _patientService)
        {
            patientService = _patientService;

            Patients = new ObservableCollection<Patient>(patientService.GetAllPatients());

            NavigateBack = new Command(async () =>
            {
                await Shell.Current.GoToAsync("///MainPage");
            });

            SearchCommand = new Command(Search);
            DeleteCommand = new Command<Patient>(Delete);

            searchDebounceTimer = new System.Timers.Timer(DebounceDelay);
            searchDebounceTimer.Elapsed += (s, e) =>
            {
                searchDebounceTimer.Stop();
                Search();
            };
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

        public void Add(Patient newPatient)
        {
            patientService.AddPatient(newPatient);

            Patients.Add(newPatient);
        }

        public void Delete(Patient patient)
        {
            if (patient != null)
            {
                patientService.DeletePatient(patient);
                Patients.Remove(patient);
            }               
        }

        private void Search()
        {
            IEnumerable<Patient> results;
            if (SearchQuery != "")
            { results = patientService.GetPatientsByName(SearchQuery); }
            else
            { results = patientService.GetAllPatients(); }
            Patients.Clear();
            foreach (var patient in results)
                Patients.Add(patient);
        }

        public void Refresh()
        {
            NotifyPropertyChanged(nameof(Patients));
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
