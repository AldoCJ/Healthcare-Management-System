using HealthcareManagementApp.Services;
using HealthcareManagementApp.ViewModels;

namespace HealthcareManagementApp.Views;

public partial class AppointmentView : ContentPage
{
	public AppointmentView()
	{
		InitializeComponent();
		
		var patientService = IPlatformApplication.Current?.Services.GetService<PatientService>();
		var physicianService = IPlatformApplication.Current?.Services.GetService<PhysicianService>();
		var appointmentService = IPlatformApplication.Current?.Services.GetService<AppointmentService>();
		if (patientService == null)
			throw new InvalidOperationException("PatientService not registered in DI container.");
		if (physicianService == null)
			throw new InvalidOperationException("PhysicianService not registered in DI container.");
		if (appointmentService == null)
			throw new InvalidOperationException("AppointmentService not registered in DI container.");
		BindingContext = new AppointmentViewModel(appointmentService, patientService, physicianService);
    }
}