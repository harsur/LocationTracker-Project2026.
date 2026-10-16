
using Microsoft.Maui.Devices.Sensors;

namespace LocationTracker.Services;

public class LocationService
{
    public async Task<Location?> TryGetCurrentLocationAsync()
    {
        var permission = await Permissions.RequestAsync<
            Permissions.LocationWhenInUse>();

        if (permission != PermissionStatus.Granted)
            return null;

        try
        {
            var request = new GeolocationRequest(
                GeolocationAccuracy.Medium,
                TimeSpan.FromSeconds(10));

            return await Geolocation.Default.GetLocationAsync(request);
        }
        catch (FeatureNotEnabledException)
        {
            return null;
        }
        catch (PermissionException)
        {
            return null;
        }
    }
}
