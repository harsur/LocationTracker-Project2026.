
using LocationTracker.Services;
using LocationTracker.Models;

namespace LocationTracker;

public partial class MainPage : ContentPage
{
    private readonly LocationService _locationService = new();

    public MainPage()
    {
        InitializeComponent();
    }

    private async void OnGetLocationClicked(object sender, EventArgs e)
    {
        try
        {
            var location = await _locationService.TryGetCurrentLocationAsync();

            if (location == null)
            {
                await DisplayAlert(
                    "Location",
                    "Location unavailable. Please check permissions and device location settings.",
                    "OK");
                return;
            }

            await DisplayAlert(
                "Current Location",
                $"Latitude: {location.Latitude}\nLongitude: {location.Longitude}",
                "OK");
        }
        catch (Exception)
        {
            await DisplayAlert(
                "Error",
                "Unable to retrieve the current location.",
                "OK");
        }
    }
}
