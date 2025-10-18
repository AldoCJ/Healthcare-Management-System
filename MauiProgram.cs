using Microsoft.Extensions.Logging;


namespace HealthcareManagementApp
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();

            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
            builder.Logging.AddDebug();

#endif
            var patientService = new Services.PatientService();
            builder.Services.AddSingleton(patientService);
            var physicianService = new Services.PhysicianService();
            builder.Services.AddSingleton(physicianService);
            var appointmentService = new Services.AppointmentService();
            builder.Services.AddSingleton(appointmentService);

            return builder.Build();
        }
    }
}
