using HealthcareManagementApp.Models;
using HealthcareManagementApp.Services;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using System.Timers;
using System.Threading.Tasks;

namespace HealthcareManagementApp.ViewModels
{
    public class PatientsManagerViewModel : INotifyPropertyChanged
    {
        private readonly PatientService patientService;

        private string searchQuery = string.Empty;
        public ObservableCollection<Patient> Patients { get; set; }

        public ICommand NavigateBack { get; }
        public ICommand NavigateToNewPatient { get; }
        public ICommand SearchCommand { get; }
        public ICommand DeleteCommand { get; }
        public ICommand EditCommand { get; }

        private System.Timers.Timer searchDebounceTimer;
        private const int DebounceDelay = 500; 

        public PatientsManagerViewModel(PatientService _patientService)
        {
            patientService = _patientService;

            Patients = new ObservableCollection<Patient>(patientService.GetAllPatients());

            NavigateBack = new Command(async () =>
            {
                await Shell.Current.GoToAsync("///MainPage");
            });

            NavigateToNewPatient = new Command(async () =>
            {
                await Shell.Current.GoToAsync(nameof(Views.NewPatientView));
            });

            SearchCommand = new Command(Search);
            DeleteCommand = new Command<Patient>(Delete);
            EditCommand = new Command<Patient>(async (patient) => await Edit(patient));

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

        private void Delete(Patient patient)
        {
            if (patient != null)
            {
                patientService.DeletePatient(patient);
                Patients.Remove(patient);
            }               
        }

        private async Task Edit(Patient patient)
        {
            if (patient != null)
            {
                var navigationParameter = new Dictionary<string, object>
                {
                    { "Patient", patient }
                };

                await Shell.Current.GoToAsync(nameof(Views.NewPatientView), navigationParameter);
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
            Patients = new ObservableCollection<Patient>(patientService.GetAllPatients());
            NotifyPropertyChanged(nameof(Patients));
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
