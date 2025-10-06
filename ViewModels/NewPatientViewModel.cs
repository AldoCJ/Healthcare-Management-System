using System;
using System.Windows.Input;
using HealthcareManagementApp.Services;
using HealthcareManagementApp.Models;
using System.ComponentModel;

namespace HealthcareManagementApp.ViewModels
{
    class NewPatientViewModel : INotifyPropertyChanged
    {
        private readonly PatientService patientService;

        // Properties for binding
        private string? name;
        private string? address;
        private DateTime birthday = DateTime.Today;
        private string? race;
        private string? gender;
        private string? medicalNotes;

        public ICommand NavigateBack { get; }
        public ICommand SaveCommand { get; }

        public NewPatientViewModel(PatientService _patientService)
        {
            patientService = _patientService;

            NavigateBack = new Command(async () =>
            {
                await Shell.Current.GoToAsync("..");
            });

            SaveCommand = new Command(Save);
        }

        public string? Name
        {
            get => name;
            set { name = value; NotifyPropertyChanged();
            }
        }

        public string? Address
        {
            get => address;
            set
            {
                address = value; NotifyPropertyChanged();
            }
        }

        public DateTime Birthday
        {
            get => birthday;
            set
            {
                birthday = value; NotifyPropertyChanged();
            }
        }

        public string? Race
        {
            get => race;
            set
            {
                race = value; NotifyPropertyChanged();
            }
        }

        public string? Gender
        {
            get => gender;
            set
            {
                gender = value; NotifyPropertyChanged();
            }
        }

        public string? MedicalNotes
        {
            get => medicalNotes;
            set
            {
                medicalNotes = value; NotifyPropertyChanged();
            }
        }


        public async void Save()
        {
            try
            {
                var patient = new Patient(
                    Name,
                    Address,
                    DateOnly.FromDateTime(Birthday),
                    Race,
                    Gender,
                    MedicalNotes
                );

                patientService.AddPatient(patient);

                // Optionally navigate back after saving
                await Shell.Current.GoToAsync("..");
            }
            catch (Exception ex)
            {
                if (Application.Current?.MainPage != null)
                {
                    await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
                }
                // Optionally, handle the case where MainPage is null
            }
            
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        private void NotifyPropertyChanged([System.Runtime.CompilerServices.CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
