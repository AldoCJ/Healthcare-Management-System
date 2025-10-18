namespace HealthcareManagementApp.Views;

public partial class PhysiciansManagerView : ContentPage
{
	public PhysiciansManagerView()
	{
		InitializeComponent();
		var physicianService = IPlatformApplication.Current.Services.GetService<Services.PhysicianService>();
		if (physicianService == null)
			throw new InvalidOperationException("PhysicianService not registered in DI container.");
        var appointmentService = IPlatformApplication.Current.Services.GetService<Services.AppointmentService>();
        if (appointmentService == null)
            throw new InvalidOperationException("PhysicianService not registered in DI container.");

        BindingContext = new ViewModels.PhysiciansManagerViewModel(physicianService, appointmentService);
	}
	private void OnSearchEntryCompleted(object sender, EventArgs e)
	{
		if (BindingContext is ViewModels.PhysiciansManagerViewModel vm)
		{
			vm.SearchCommand.Execute(null);
		}
	}
	protected override void OnAppearing()
	{
		base.OnAppearing();
		if (BindingContext is ViewModels.PhysiciansManagerViewModel vm)
		{
			vm.Refresh();
        }
    }
}