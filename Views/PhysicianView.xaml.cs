namespace HealthcareManagementApp.Views;

public partial class PhysicianView : ContentPage
{
	public PhysicianView()
	{
		InitializeComponent();

		var physicianService = IPlatformApplication.Current?.Services.GetService<Services.PhysicianService>();
		if (physicianService == null)
			throw new InvalidOperationException("PhysicianService not registered in DI container.");
		BindingContext = new ViewModels.PhysicianViewModel(physicianService);
    }
}