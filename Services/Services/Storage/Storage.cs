using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Services.Models.Route;
using SQLite;

namespace Services.Storage
{
    public class Storage : IStorage
    {
        private readonly SQLiteAsyncConnection sqLiteConnection;

        public Storage()
        {
            var databasePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "pomidoros.db");

            sqLiteConnection = new SQLiteAsyncConnection(databasePath);

            sqLiteConnection.CreateTableAsync<RouteInfoDTO>();
            sqLiteConnection.CreateTableAsync<LocationDTO>();
        }

        public async Task AddRouteInfo(RouteInfoModel routeInfo)
        {
            var coords = routeInfo.Coordinates.Select(i => new LocationDTO
            {
                RouteId = routeInfo.Id,
                Lat = i.Item1,
                Lon = i.Item2
            });
            await sqLiteConnection.InsertAllAsync(coords);

            await sqLiteConnection.InsertAsync(new RouteInfoDTO
            {
                Id = routeInfo.Id,
                OrderId = routeInfo.OrderId,
                UserId = routeInfo.UserId
            });
        }

        public async Task<RouteInfoModel> GetRouteInfo(int id)
        {
            var routes = await sqLiteConnection.Table<RouteInfoDTO>().Where(i => i.Id == id).ToListAsync();
            if (routes.Count != 1)
            {
                return null;
            }

            var coords1 = await sqLiteConnection.Table<LocationDTO>().Where(i => i.RouteId == id).ToListAsync();
            var coords2 = coords1.Select(i => new Tuple<double, double>(i.Lat, i.Lon)).ToList();

            return new RouteInfoModel
            {
                Id = id,
                OrderId = routes[0].OrderId,
                UserId = routes[0].UserId,
                Coordinates = coords2
            };
        }

        public async Task RemoveAll()
        {
            await sqLiteConnection.DeleteAllAsync<RouteInfoDTO>();
            await sqLiteConnection.DeleteAllAsync<LocationDTO>();
        }
    }
}
