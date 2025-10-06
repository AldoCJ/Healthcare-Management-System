using HealthcareManagementApp.ViewModels;
using HealthcareManagementApp.Services;
using Microsoft.Maui.Controls;

namespace HealthcareManagementApp.Views;

public partial class NewPatientView : ContentPage
{
    public NewPatientView()
    {
        InitializeComponent();

        var patientService = IPlatformApplication.Current?.Services.GetService<PatientService>();
        if (patientService == null)
            throw new InvalidOperationException("PatientService not registered in DI container.");

        BindingContext = new NewPatientViewModel(patientService);
    }
}