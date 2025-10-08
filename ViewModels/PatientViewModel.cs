using System;
using System.Windows.Input;
using HealthcareManagementApp.Services;
using HealthcareManagementApp.Models;
using System.ComponentModel;

namespace HealthcareManagementApp.ViewModels
{
    [QueryProperty(nameof(SelectedPatient), "Patient")]
    public class PatientViewModel : INotifyPropertyChanged
    {
        private readonly PatientService patientService;
        private Patient? selectedPatient;

        // Properties for binding
        private string? name;
        private string? address;
        private DateTime birthday = DateTime.Today;
        private string? race;
        private string? gender;
        private string? medicalNotes;

        public ICommand NavigateBack { get; }
        public ICommand SaveCommand { get; }

        public PatientViewModel(PatientService _patientService, Patient? patient = null)
        {
            patientService = _patientService;

            NavigateBack = new Command(async () =>
            {
                await Shell.Current.GoToAsync("..");
            });

            SaveCommand = new Command(Save);

        }

        public Patient? SelectedPatient
        {
            get => selectedPatient;
            set
            {
                selectedPatient = value;
                if (selectedPatient != null)
                {
                    Name = selectedPatient.Name;
                    Address = selectedPatient.Address;
                    Birthday = selectedPatient.Birthday.ToDateTime(new TimeOnly(0, 0));
                    Race = selectedPatient.Race;
                    Gender = selectedPatient.Gender;
                    MedicalNotes = selectedPatient.MedicalNotes;
                }
                NotifyPropertyChanged();
            }
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
                if (selectedPatient != null)
                {
                    // Update existing patient
                    selectedPatient.Name = Name;
                    selectedPatient.Address = Address;
                    selectedPatient.Birthday = DateOnly.FromDateTime(Birthday);
                    selectedPatient.Race = Race;
                    selectedPatient.Gender = Gender;
                    selectedPatient.MedicalNotes = MedicalNotes;

                    patientService.UpdatePatient(selectedPatient);
                }

                else
                {
                    var newPatient = new Patient(
                    Name,
                    Address,
                    DateOnly.FromDateTime(Birthday),
                    Race,
                    Gender,
                    MedicalNotes
                );

                    patientService.AddPatient(newPatient);
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
