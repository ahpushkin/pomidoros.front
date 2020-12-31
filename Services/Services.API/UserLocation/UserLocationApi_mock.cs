using System.Threading;
using System.Threading.Tasks;

namespace Services.API.UserLocation
{
    public class UserLocationApi_mock : IUserLocationApi
    {
        public async Task SendCurrentLocationAsync(int routeId, string latitude, string longitude, CancellationToken token)
        {
            await Task.Delay(1000);
            System.Diagnostics.Debug.WriteLine($"Saved route ({routeId}) coordinate: {latitude}:{longitude}");
        }

        public async Task<string> GetRouteInfoAsync(int orderId, int userId, string startLatitude, string startLongitude, string endLatitude, string endLongitude, CancellationToken token)
        {
            await Task.Delay(100);
            System.Diagnostics.Debug.WriteLine($"Get route for order ({orderId}), user {userId}, coordinates: {startLatitude}:{startLongitude}-{endLatitude}:{endLongitude}");

            return "{ \"geo_direction_result\": [ { \"bounds\": { \"northeast\": { \"lat\": 50.43716269999999, \"lng\": 30.334728 }, \"southwest\": { \"lat\": 50.4341283, \"lng\": 30.3311809 } }, \"copyrights\": \"Map data ©2020 Google\", \"legs\": [ { \"distance\": { \"text\": \"0,5 км\", \"value\": 536 }, \"duration\": { \"text\": \"1 мин.\", \"value\": 71 }, \"end_address\": \"вулиця Богдана Хмельницького, 28, Петропавлівська Борщагівка, Київська обл., Украина, 08130\", \"end_location\": { \"lat\": 50.43716269999999, \"lng\": 30.3337534 }, \"start_address\": \"вулиця Садова, 1Бк1, Петропавлівська Борщагівка, Київська обл., Украина, 08130\", \"start_location\": { \"lat\": 50.4341283, \"lng\": 30.3313164 }, \"steps\": [ { \"distance\": { \"text\": \"25 м\", \"value\": 25 }, \"duration\": { \"text\": \"1 мин.\", \"value\": 6 }, \"end_location\": { \"lat\": 50.4343369, \"lng\": 30.3311809 }, \"html_instructions\": \"Направляйтесь на <b>север</b> по <b>вулиця Садова</b> в сторону <b>вулиця Соборна</b>\", \"polyline\": { \"points\": \"ilyrHwacxDi@Z\" }, \"start_location\": { \"lat\": 50.4341283, \"lng\": 30.3313164 }, \"travel_mode\": \"DRIVING\" }, { \"distance\": { \"text\": \"0,3 км\", \"value\": 263 }, \"duration\": { \"text\": \"1 мин.\", \"value\": 29 }, \"end_location\": { \"lat\": 50.4350239, \"lng\": 30.334728 }, \"html_instructions\": \"Поверните <b>направо</b> на <b>вулиця Соборна</b>\", \"maneuver\": \"turn-right\", \"polyline\": { \"points\": \"smyrH{`cxDIq@Iu@_@aDIu@SsBo@oFEa@\" }, \"start_location\": { \"lat\": 50.4343369, \"lng\": 30.3311809 }, \"travel_mode\": \"DRIVING\" }, { \"distance\": { \"text\": \"0,2 км\", \"value\": 248 }, \"duration\": { \"text\": \"1 мин.\", \"value\": 36 }, \"end_location\": { \"lat\": 50.43716269999999, \"lng\": 30.3337534 }, \"html_instructions\": \"Поверните <b>налево</b> на <b>вул. Богдана Хмельницького</b><div style=\\\"font - size:0.9em\\\">Пункт назначения будет справа</div>\", \"maneuver\": \"turn-left\", \"polyline\": { \"points\": \"{qyrHawcxDWHQHYJSD_@Jk@PIB[JgA^y@\\_Bp@\" }, \"start_location\": { \"lat\": 50.4350239, \"lng\": 30.334728 }, \"travel_mode\": \"DRIVING\" } ], \"traffic_speed_entry\": [], \"via_waypoint\": [] } ], \"overview_polyline\": { \"points\": \"ilyrHwacxDi@ZIq@i@wEsA{LwAd@qBl@aC|@_Bp@\" }, \"summary\": \"вулиця Соборна и вул. Богдана Хмельницького\", \"warnings\": [], \"waypoint_order\": [] } ], \"id\": 1, \"created\": \"2020-12-30T17:28:57.297001+02:00\", \"modified\": \"2020-12-30T17:28:57.297066+02:00\", \"start_lat_courier\": \"50.43404540000000000\", \"start_lon_courier\": \"30.33100200000000000\", \"finish_point_lat\": \"50.43719550000000000\", \"finish_point_lon\": \"30.33391100000000000\", \"status\": \"delivered\", \"order\": 1, \"courier_user\": 1 }";
        }
    }
}
