using HealthcareManagementApp.ViewModels;

namespace HealthcareManagementApp
{
    public partial class MainPage : ContentPage
    {

        public MainPage()
        {
            InitializeComponent();
            BindingContext = new MainViewModel();
        }

    }

}
