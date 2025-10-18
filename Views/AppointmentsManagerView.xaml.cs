using HealthcareManagementApp.ViewModels;
using HealthcareManagementApp.Services;

namespace HealthcareManagementApp.Views;

public partial class AppointmentsManagerView : ContentPage
{
	public AppointmentsManagerView()
	{
		InitializeComponent();
		var appointmentService = IPlatformApplication.Current.Services.GetService<AppointmentService>();
		if (appointmentService == null)
			throw new InvalidOperationException("AppointmentService not registered in DI container.");

        var patientService = IPlatformApplication.Current.Services.GetService<PatientService>();
        if (patientService == null)
            throw new InvalidOperationException("PatientService not registered in DI container.");

		var physicianService = IPlatformApplication.Current.Services.GetService<PhysicianService>();
		if (physicianService == null)
			throw new InvalidOperationException("PhysicianService not registered in DI container.");

        BindingContext = new AppointmentsManagerViewModel(appointmentService, patientService, physicianService);
    }

	private void OnSearchEntryCompleted(object sender, EventArgs e)
	{
		if (BindingContext is AppointmentsManagerViewModel vm)
		{
			vm.SearchCommand.Execute(null);
		}
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        if (BindingContext is AppointmentsManagerViewModel vm)
        {
            vm.Refresh();
        }
    }
}