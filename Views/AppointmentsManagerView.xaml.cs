using HealthcareManagementApp.ViewModels;

namespace HealthcareManagementApp.Views;

public partial class AppointmentsManagerView : ContentPage
{
	public AppointmentsManagerView()
	{
		InitializeComponent();
		BindingContext = new AppointmentsManagerViewModel();
    }

	private void OnSearchEntryCompleted(object sender, EventArgs e)
	{
		if (BindingContext is AppointmentsManagerViewModel vm)
		{
			vm.SearchCommand.Execute(null);
		}
    }
}