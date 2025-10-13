using HealthcareManagementApp.Models;
using HealthcareManagementApp.Services;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using HealthcareManagementApp.Views;

namespace HealthcareManagementApp.ViewModels
{
    public class PhysiciansManagerViewModel : INotifyPropertyChanged
    {
        private readonly PhysicianService physicianService;

        private string searchQuery = string.Empty;
        public ObservableCollection<Physician> Physicians { get; set; }

        public ICommand NavigateBack { get; }
        public ICommand NavigateToNewPhysician { get; }
        public ICommand SearchCommand { get; }
        public ICommand DeleteCommand { get; }
        public ICommand EditCommand { get; }

        private System.Timers.Timer searchDebounceTimer;
        private const int DebounceDelay = 500;

        public PhysiciansManagerViewModel(PhysicianService _physicianService)
        {
            physicianService = _physicianService;

            Physicians = new ObservableCollection<Physician>(physicianService.GetAllPhysicians());

            NavigateBack = new Command(async () =>
            {
                await Shell.Current.GoToAsync("..");
            });

            NavigateToNewPhysician = new Command(async () =>
            {
                await Shell.Current.GoToAsync(nameof(PhysicianView));
            });

            SearchCommand = new Command(Search);
            DeleteCommand = new Command<Physician>(Delete);
            EditCommand = new Command<Physician>(async (physician) => await Edit(physician));

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

        private void Delete(Physician physician)
        {
            if (physician != null)
            {
                physicianService.DeletePhysician(physician);
                Physicians.Remove(physician);
            }
        }

        private async Task Edit(Physician physician)
        {
            if (physician != null)
            {
                var navigationParameter = new Dictionary<string, object>
                {
                    { "Physician", physician }
                };

                await Shell.Current.GoToAsync(nameof(Views.PhysicianView), navigationParameter);
            }
        }

        private void Search()
        {
            IEnumerable<Physician> results;
            if (SearchQuery != "")
                results = physicianService.GetPhysiciansByName(SearchQuery);
            else
                results = physicianService.GetAllPhysicians();

            Physicians.Clear();
            foreach (var physician in results)
                Physicians.Add(physician);
        }

        public void Refresh()
        {
            Physicians = new ObservableCollection<Physician>(physicianService.GetAllPhysicians());
            NotifyPropertyChanged(nameof(Physicians));
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
