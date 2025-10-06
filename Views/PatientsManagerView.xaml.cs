using HealthcareManagementApp.ViewModels;
using HealthcareManagementApp.Services;
using Microsoft.Maui.Controls;

namespace HealthcareManagementApp.Views;

public partial class PatientsManagerView : ContentPage
{
	public PatientsManagerView()
	{
		InitializeComponent();

		var patientService = IPlatformApplication.Current.Services.GetService<PatientService>();
        if (patientService == null)
            throw new InvalidOperationException("PatientService not registered in DI container.");
        BindingContext = new PatientsManagerViewModel(patientService);
    }

    private void OnSearchEntryCompleted(object sender, EventArgs e)
    {
        if (BindingContext is HealthcareManagementApp.ViewModels.PatientsManagerViewModel vm)
        {
            vm.SearchCommand.Execute(null);
        }
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        if (BindingContext is PatientsManagerViewModel vm)
        {
            vm.Refresh();
        }
    }

}