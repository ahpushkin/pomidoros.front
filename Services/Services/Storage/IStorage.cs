using System.Threading.Tasks;
using Services.Models.Route;

namespace Services.Storage
{
    public interface IStorage
    {
        Task AddRouteInfo(RouteInfoModel routeInfo);

        Task<RouteInfoModel> GetRouteInfo(int id);

        Task RemoveAll();
    }
}
