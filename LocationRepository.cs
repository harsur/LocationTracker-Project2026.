using SQLite;
using LocationTracker.Models;

namespace LocationTracker.Data
{
    public class LocationRepository
    {
        private readonly SQLiteAsyncConnection _database;

        public LocationRepository(string databasePath)
        {
            _database = new SQLiteAsyncConnection(databasePath);
            _database.CreateTableAsync<LocationRecord>().Wait();
        }

        public Task<List<LocationRecord>> GetLocationsAsync()
        {
            return _database.Table<LocationRecord>()
                            .OrderByDescending(x => x.RecordedAt)
                            .ToListAsync();
        }

        public Task<int> SaveLocationAsync(LocationRecord location)
        {
            return _database.InsertAsync(location);
        }

        public Task<int> DeleteLocationAsync(LocationRecord location)
        {
            return _database.DeleteAsync(location);
        }
    }
}
