using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Services.Models.Enums;
using Services.Models.Orders;
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

            sqLiteConnection.CreateTableAsync<OrderContentDTO>();
            sqLiteConnection.CreateTableAsync<OrderDTO>();
            sqLiteConnection.CreateTableAsync<RouteInfoDTO>();
            sqLiteConnection.CreateTableAsync<LocationDTO>();
        }

        public async Task AddOrder(FullOrderModel order)
        {
            var contentItems = order.Contents.Select(i => new OrderContentDTO
            {
                OrderId = Convert.ToInt64(order.Number),
                Name = i.Name,
                Count = i.Count,
                Price = (int)i.Price
            });
            await sqLiteConnection.InsertAllAsync(contentItems);

            await sqLiteConnection.InsertAsync(new OrderDTO(order));
        }

        public async Task UpdateOrder(FullOrderModel order)
        {
            await sqLiteConnection.UpdateAsync(new OrderDTO(order));
        }

        public async Task<FullOrderModel> GetOrder(long id)
        {
            var orders = await sqLiteConnection.Table<OrderDTO>().Where(i => i.Id == id).ToListAsync();
            if (orders.Count != 1)
            {
                return null;
            }

            var start = orders[0].StartAddress.Split(';');
            var delivery = orders[0].DeliveryAddress.Split(';');
            if (start.Length != 2 || delivery.Length != 2)
            {
                return null;
            }

            if (!Enum.IsDefined(typeof(EOrderStatus), orders[0].Status)
                || !Enum.IsDefined(typeof(EOrderType), orders[0].Type))
            {
                return null;
            }

            var contentItems1 = await sqLiteConnection.Table<OrderContentDTO>().Where(i => i.OrderId == id).ToListAsync();
            var contentItems2 = contentItems1.Select(i => new OrderContentModel
            {
                Name = i.Name,
                Count = i.Count,
                Price = i.Price
            }).ToList();

            return new FullOrderModel
            {
                Number = $"{id}",
                OrderNumber = $"{orders[0].OrderNumber}",
                OrderStatus = (EOrderStatus)orders[0].Status,
                StartCity = start[0],
                StartAddress = start[1],
                DeliveryCity = delivery[0],
                DeliveryAddress = delivery[1],
                Distance = orders[0].Distance,
                ClientNumber = orders[0].ClientPhone,
                Comments = orders[0].Comments,
                Contents = contentItems2,
                AmountPrice = orders[0].Price,
                Type = (EOrderType)orders[0].Type,
                EndTime = DateTimeOffset.FromUnixTimeSeconds(orders[0].EndTime),
                IsClientLiked = orders[0].ClientLiked
            };
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
            await sqLiteConnection.DeleteAllAsync<OrderContentDTO>();
            await sqLiteConnection.DeleteAllAsync<OrderDTO>();
            await sqLiteConnection.DeleteAllAsync<RouteInfoDTO>();
            await sqLiteConnection.DeleteAllAsync<LocationDTO>();
        }
    }
}
