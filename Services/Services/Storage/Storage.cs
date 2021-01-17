using System;
using System.Collections.Generic;
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
            if (order.Contents != null)
            {
                var contentItems = order.Contents.Select(i => new OrderContentDTO
                {
                    OrderId = Convert.ToInt64(order.Number),
                    Name = i.Name,
                    Count = i.Count,
                    Price = (int)i.Price
                });
                await sqLiteConnection.InsertAllAsync(contentItems);
            }

            await sqLiteConnection.InsertAsync(new OrderDTO(order));
        }

        public async Task UpdateOrder(FullOrderModel order)
        {
            await sqLiteConnection.UpdateAsync(new OrderDTO(order));
        }

        public async Task<FullOrderModel> GetOrder(long id)
        {
            var orders = await sqLiteConnection.Table<OrderDTO>().Where(i => i.Number == id).ToListAsync();
            if (orders.Count != 1)
            {
                return null;
            }

            var contentItemsDto = await sqLiteConnection.Table<OrderContentDTO>().Where(i => i.OrderId == id).ToListAsync();
            var contentItems = contentItemsDto.Select(i => new OrderContentModel
            {
                Name = i.Name,
                Count = i.Count,
                Price = i.Price
            }).ToList();

            return orders[0].GetModel(contentItems);
        }

        public async Task<IEnumerable<FullOrderModel>> GetAllOrders(bool isHistoryOrders)
        {
            List<OrderDTO> orders;
            if (isHistoryOrders)
            {
                orders = await sqLiteConnection.Table<OrderDTO>().Where(i => i.Status == 0 || i.Status == 1 || i.Status == 3).ToListAsync();
            }
            else
            {
                orders = await sqLiteConnection.Table<OrderDTO>().Where(i => i.Status == 2 || i.Status == 4 || i.Status == 5).ToListAsync();
            }

            var result = new List<FullOrderModel>();
            foreach (var orderDto in orders)
            {
                var contentItemsDto = await sqLiteConnection.Table<OrderContentDTO>().Where(i => i.OrderId == orderDto.Number).ToListAsync();
                var contentItems = contentItemsDto.Select(i => new OrderContentModel
                {
                    Name = i.Name,
                    Count = i.Count,
                    Price = i.Price
                }).ToList();

                var order = orderDto.GetModel(contentItems);
                if (order != null)
                {
                    result.Add(order);
                }
            }
            return result;
        }

        public async Task AddRouteInfo(RouteInfoModel routeInfo)
        {
            await sqLiteConnection.InsertAsync(new RouteInfoDTO(routeInfo));

            var coords = routeInfo.Coordinates.Select(i => new LocationDTO
            {
                RouteId = routeInfo.Id,
                Lat = i.Item1,
                Lon = i.Item2
            });
            await sqLiteConnection.InsertAllAsync(coords);
        }

        public async Task<RouteInfoModel> GetRouteInfo(long id)
        {
            var routes = await sqLiteConnection.Table<RouteInfoDTO>().Where(i => i.Id == id).ToListAsync();
            if (routes.Count != 1)
            {
                return null;
            }

            return await GetRouteInfo(routes[0]);
        }

        public async Task<RouteInfoModel> GetRouteInfoForOrder(long orderId)
        {
            var routes = await sqLiteConnection.Table<RouteInfoDTO>().Where(i => i.OrderId == orderId).ToListAsync();
            if (routes.Count != 1)
            {
                return null;
            }

            return await GetRouteInfo(routes[0]);
        }

        public async Task RemoveAll()
        {
            await sqLiteConnection.DeleteAllAsync<OrderContentDTO>();
            await sqLiteConnection.DeleteAllAsync<OrderDTO>();
            await sqLiteConnection.DeleteAllAsync<RouteInfoDTO>();
            await sqLiteConnection.DeleteAllAsync<LocationDTO>();
        }

        private async Task<RouteInfoModel> GetRouteInfo(RouteInfoDTO routeInfoDTO)
        {
            var coords1 = await sqLiteConnection.Table<LocationDTO>().Where(i => i.RouteId == routeInfoDTO.Id).ToListAsync();
            var coords2 = coords1.Select(i => new Tuple<double, double>(i.Lat, i.Lon)).ToList();

            return new RouteInfoModel
            {
                Id = routeInfoDTO.Id,
                OrderId = routeInfoDTO.OrderId,
                UserId = routeInfoDTO.UserId,
                Coordinates = coords2
            };
        }
    }
}
