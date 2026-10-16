
using SQLite;
using LocationTracker.Models;

namespace LocationTracker.Services;

public sealed class LocationRepository
{
    private readonly SQLiteAsyncConnection _database;

    public LocationRepository(string databasePath)
    {
        _database = new SQLiteAsyncConnection(databasePath);
    }

    // Call once during app startup before using the repository.
    public Task InitializeAsync() =>
        _database.CreateTableAsync<LocationRecord>();

    public Task<int> SaveAsync(double latitude, double longitude)
    {
        if (latitude < -90 || latitude > 90)
            throw new ArgumentOutOfRangeException(nameof(latitude));

        if (longitude < -180 || longitude > 180)
            throw new ArgumentOutOfRangeException(nameof(longitude));

        var record = new LocationRecord
        {
            Latitude = latitude,
            Longitude = longitude,
            CapturedAtUtc = DateTime.UtcNow
        };

        return _database.InsertAsync(record);
    }

    public Task<List<LocationRecord>> GetAllAsync() =>
        _database.Table<LocationRecord>()
                 .OrderBy(x => x.CapturedAtUtc)
                 .ToListAsync();
}
