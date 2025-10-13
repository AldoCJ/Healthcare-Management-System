using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using HealthcareManagementApp.Models;
using HealthcareManagementApp.Services;

namespace HealthcareManagementApp.ViewModels
{
    [QueryProperty(nameof(SelectedPhysician), "Physician")]
    public class PhysicianViewModel : INotifyPropertyChanged
    {
        private readonly PhysicianService physicianService;
        private Physician? selectedPhysician;

        // Properties for binding
        private string? name;
        private string? licenseNumber;
        private DateTime gradDate = DateTime.Today;
        private string? specializations;

        public ICommand NavigateBack { get; }
        public ICommand SaveCommand { get; }

        public PhysicianViewModel(PhysicianService _physicianService, Physician? physician = null)
        {
            physicianService = _physicianService;

            NavigateBack = new Command(async () =>
            {
                await Shell.Current.GoToAsync("..");
            });

            SaveCommand = new Command(Save);
        }

        public Physician? SelectedPhysician
        {
            get => selectedPhysician;
            set
            {
                selectedPhysician = value;
                if (selectedPhysician != null)
                {
                    Name = selectedPhysician.Name;
                    LicenseNumber = selectedPhysician.LicenseNumber;
                    GradDate = selectedPhysician.GradDate.ToDateTime(new TimeOnly(0, 0));
                    Specializations = selectedPhysician.Specializations;
                }
                NotifyPropertyChanged();
            }
        }

        public string? Name
        {
            get => name;
            set { name = value; NotifyPropertyChanged(); }
        }

        public string? LicenseNumber
        {
            get => licenseNumber;
            set { licenseNumber = value; NotifyPropertyChanged(); }
        }

        public DateTime GradDate
        {
            get => gradDate;
            set { gradDate = value; NotifyPropertyChanged(); }
        }

        public string? Specializations
        {
            get => specializations;
            set { specializations = value; NotifyPropertyChanged(); }
        }

        public async void Save()
        {
            try
            {
                if (selectedPhysician != null)
                {
                    // Update existing physician
                    selectedPhysician.Name = Name;
                    selectedPhysician.LicenseNumber = LicenseNumber;
                    selectedPhysician.GradDate = DateOnly.FromDateTime(GradDate);
                    selectedPhysician.Specializations = Specializations;

                    physicianService.UpdatePhysician(selectedPhysician);
                }
                else
                {
                    var newPhysician = new Physician(
                        Name,
                        LicenseNumber,
                        DateOnly.FromDateTime(GradDate),
                        Specializations
                    );

                    physicianService.AddPhysician(newPhysician);
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
