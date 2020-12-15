using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Core.Commands;
using Core.Extensions;
using Core.Navigation;
using GeoJSON.Net.Feature;
using GeoJSON.Net.Geometry;
using Naxam.Controls.Forms;
using Naxam.Mapbox;
using Naxam.Mapbox.Annotations;
using Naxam.Mapbox.Layers;
using Naxam.Mapbox.Sources;
using Pomidoros.Resources;
using Pomidoros.View;
using Pomidoros.View.Orders;
using Pomidoros.ViewModel.Base;
using Services.Models.Orders;
using Xamarin.Forms;

namespace Pomidoros.ViewModel.Orders
{
    public class ReviewOrderPageViewModel : BaseViewModel, IParametrized
    {
        public ReviewOrderPageViewModel()
        {
            ZoomLevel = 15;

            DidFinishLoadingStyleCommand = new AsyncCommand<MapStyle>(DidFinishLoadingStyle);
        }

        public void PassParameters(NavigationParameters parameters)
        {
            if (parameters.TryGetParameter<FullOrderModel>("order", out var order))
            {
                Order = order;
                Title = string.Format(LocalizationStrings.OrderNumberTitleFormat, order.OrderNumber);
            }
        }

        private FullOrderModel _order;
        public FullOrderModel Order
        {
            get => _order;
            set => SetProperty(ref _order, value);
        }

        public IMapFunctions MapFunctions { get; set; }

        public ObservableCollection<Annotation> Annotations { get; } = new ObservableCollection<Annotation>();

        public double ZoomLevel { get; }

        LatLng _center = new LatLng(49.9977729, 36.2413953);
        public LatLng Center
        {
            get => _center;
            set => SetProperty(ref _center, value);
        }

        public ICommand ShowRouteCommand => new AsyncCommand(OnShowRouteCommand);
        public ICommand OrderContentCommand => new AsyncCommand(OnOrderContentCommand);
        public ICommand DidFinishLoadingStyleCommand { get; }

        async Task DidFinishLoadingStyle(MapStyle mapStyle)
        {
            var routeCoordinates = await GetRouteCoordinates();

            if (routeCoordinates.Count > 0)
            {
                Center = new LatLng(routeCoordinates[0].Latitude, routeCoordinates[0].Longitude);

                AddEndPoints(routeCoordinates, Annotations, "geo.png", "geo2.png");

                AddRoute(routeCoordinates, MapFunctions, Color.FromHex("#96A637"));
            }
        }

        async Task<IList<IPosition>> GetRouteCoordinates()
        {
            await Task.Delay(1000);

            return new List<IPosition>
                {
                    new Position( 49.9977729, 36.2413953 ),
                    new Position( 49.9978729, 36.2416953 ),
                    new Position( 49.9979729, 36.2417953 )
                };
        }

        void AddEndPoints(IList<IPosition> routeCoordinates, IList<Annotation> annotations, ImageSource startImage, ImageSource endImage)
        {
            var start = routeCoordinates[0];
            var end = routeCoordinates[routeCoordinates.Count - 1];

            var startAnnotation = new SymbolAnnotation()
            {
                Coordinates = new LatLng(start.Latitude, start.Longitude),
                IconImage = startImage,
                IconSize = 1
            };
            annotations.Add(startAnnotation);

            var endAnnotation = new SymbolAnnotation()
            {
                Coordinates = new LatLng(end.Latitude, end.Longitude),
                IconImage = endImage,
                IconSize = 2
            };
            annotations.Add(endAnnotation);
        }

        void AddRoute(IEnumerable<IPosition> routeCoordinates, IMapFunctions mapFunctions, Color lineColor)
        {
            var featureCollection = new FeatureCollection();
            var geometry = new LineString(routeCoordinates);
            var feature = new Feature(geometry);
            featureCollection.Features.Add(feature);

            var sourceId = Guid.NewGuid().ToString();
            var source = new GeoJsonSource()
            {
                Id = sourceId,
                Data = featureCollection
            };
            mapFunctions.AddSource(source);

            var lineLayer = new LineLayer(Guid.NewGuid().ToString(), sourceId)
            {
                LineWidth = 5,
                LineColor = lineColor
            };
            mapFunctions.AddLayer(lineLayer);
        }

        private Task OnShowRouteCommand(object arg)
        {
            return Navigation.PushAsync(new MapPage());
        }

        private Task OnOrderContentCommand(object arg)
        {
            return Navigation.PushAsync(new OrderContentPage(), Order, "order");
        }
    }
}