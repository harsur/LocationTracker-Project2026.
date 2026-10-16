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
        var location = await _locationService.GetCurrentLocationAsync();

        if (location == null)
        {
            await DisplayAlert(
                "Location",
                "Location unavailable. Check permissions and device settings.",
                "OK");
            return;
        }

        var record = new LocationRecord
        {
            Latitude = location.Latitude,
            Longitude = location.Longitude,
            RecordedAt = DateTime.Now
        };

        await DisplayAlert(
            "Current Location",
            $"Latitude: {record.Latitude}\nLongitude: {record.Longitude}",
            "OK");
    }
}
