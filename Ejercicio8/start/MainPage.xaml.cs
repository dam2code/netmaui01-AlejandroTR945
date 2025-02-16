namespace WeatherClient
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void btnRefresh_Clicked(object sender, EventArgs e)
        {
            btnRefresh.IsEnabled = false;
            actIsBusy.IsRunning = true;

            // Asigna el resultado al BindingContext en lugar de actualizar los controles directamente
            BindingContext = await Services.WeatherServer.GetWeather(txtPostalCode.Text);

            btnRefresh.IsEnabled = true;
            actIsBusy.IsRunning = false;
        }
    }
}
