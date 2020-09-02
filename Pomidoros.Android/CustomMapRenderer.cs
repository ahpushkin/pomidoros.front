using System;
using System.Collections.Generic;
using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using Pomidoros.Controller;
using Pomidoros.Model;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Maps.Android;
using Xamarin.Forms.Platform.Android;
using static Android.Gms.Maps.GoogleMap;

namespace Pomidoros.Droid
{
    public class CustomMapRenderer : MapRenderer, IOnMapReadyCallback, IOnMarkerClickListener
    {
        
        public string TopLeft { get; set; }
        public string BottomRight { get; set; }
        List<Marker> markers;
        GoogleMap map;
        Polyline polyline;
        public double CacheLat { get; set; }
        public double CacheLong { get; set; }
        List<Polyline> PolylineBuffer = new List<Polyline>();
        bool isDrawn;
        public static Random rand = new Random();
        public static Android.Graphics.Color GetRandomColor()
        {
            int hue = rand.Next(255);
            Android.Graphics.Color color = Android.Graphics.Color.HSVToColor(
                new[] {
          hue,
          1.0f,
          1.0f,
                }
            );
            return color;
        }
        protected override void OnElementChanged(ElementChangedEventArgs<Map> e)
        {
            base.OnElementChanged(e);
            if (e.OldElement != null)
            {
                // Unsubscribe  
            }

            if (e.NewElement != null)
            {
                ((MapView)Control).GetMapAsync(this);
            }
        }

        /// <summary>  
        /// this event notifies camerachange to viewModel by using OnVisibleRegionChanged delegate  
        /// </summary>  
        /// <param name="sender"></param>  
        /// <param name="e"></param>  
        private async void Map_CameraChange(object sender, CameraChangeEventArgs e)
        {
            map = sender as GoogleMap;
            var zoom = e.Position.Zoom;
            if (((CustomMap)this.Element) != null && map != null)
            {
                var projection = map.Projection.VisibleRegion;

                var far_right_lat = Math.Round(projection.FarLeft.Latitude, 2).ToString();
                var far_right_long = Math.Round(projection.FarLeft.Longitude, 2).ToString();

                var near_left_lat = Math.Round(projection.NearRight.Latitude, 2).ToString();
                var near_left_long = Math.Round(projection.NearRight.Longitude, 2).ToString();

                var centerLatLong = projection.LatLngBounds.Center;
                if (centerLatLong != null)
                {
                    App.CurrentLat = centerLatLong.Latitude;
                    App.CurrentLong = centerLatLong.Longitude;
                    App.CurrentZoomLevel = zoom;
                }
                var near_left = near_left_lat + "," + near_left_long;
                var near_Right = far_right_lat + "," + far_right_long;
                await ((CustomMap)this.Element).OnVisibleRegionChanged(near_left, near_Right);
            }
        }

        /// <summary>  
        /// This method will detect which colllection is changed at runtime  
        /// and will help to redraw map accordingly.  
        /// RouteCoordinatesProperty is used to draw tracks as and when information of track changes from HomeViewModel.  
        /// CustomPin property is used to redraw pins as and when collection of custom pin changes from HomeViewModel.  
        /// </summary>  
        /// <param name="sender"></param>  
        /// <param name="e"></param>  
        protected override void OnElementPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            if (this.Element == null || this.Control == null)
                return;
            if (e.PropertyName == CustomMap.RouteCoordinatesProperty.PropertyName)
            {
                var nativeMap = Control as MapView;

                if (((CustomMap)this.Element).RouteCoordinates == null)
                {
                    RemovePlolyline();
                }
                else
                    if (((CustomMap)this.Element).RouteCoordinates.Count == 0)
                {
                    RemovePlolyline();
                }
                else
                {
                    UpdatePolyLine();
                }
            }
            if (e.PropertyName == CustomMap.CustomPinsProperty.PropertyName)
            {
                var nativeMap = Control as MapView;
                if (markers != null)
                {
                    if (markers.Count > 0)
                        markers.ForEach(x => x.Remove());
                }
                SetMapMarkers();
            }
        }
        private void SetMapMarkers()
        {
            markers = new List<Marker>();
            if (((CustomMap)this.Element).CustomPins == null)
                return;
            foreach (var custompin in ((CustomMap)this.Element).CustomPins)
            {
                var marker = new MarkerOptions();
                marker.SetPosition(new LatLng(custompin.pin.Position.Latitude, custompin.pin.Position.Longitude));
                marker.SetTitle(custompin.pin.Label);
                marker.SetSnippet(custompin.pin.Address);
                var resource = typeof(Resource.Drawable).GetField(custompin.pinImage);
                int resourceId = 0;
                if (resource != null)
                {
                    resourceId = (int)resource.GetValue(custompin.pinImage);
                }
                if (resourceId != 0)
                {
                    marker.SetIcon(BitmapDescriptorFactory.FromResource(resourceId));
                    map.SetOnMarkerClickListener(this);
                    Marker m = map.AddMarker(marker);
                    markers.Add(m);
                }
            }
            isDrawn = true;
        }

        /// <summary>  
        /// this is OnMarker click event it occurs when pin is tapped  
        /// There is PinClicked Action which is implemented in ViewModel.  
        /// this provision allows us to do whatever we want from viewModel after the event   
        /// Occurs.  
        /// </summary>  
        /// <param name="marker"></param>  
        /// <returns></returns>  
        public bool OnMarkerClick(Marker marker)
        {
            SignDetails _markerDetails = new SignDetails();
            foreach (var pin in ((CustomMap)this.Element).CustomPins)
            {
                if (pin.pin.Position.Latitude == marker.Position.Latitude)
                {
                    if (pin.pin.Position.Longitude == marker.Position.Longitude)
                    {
                        if (!string.IsNullOrEmpty(pin.pin.Label))
                        {
                            if (pin.pin.Label == marker.Title)
                            {
                                _markerDetails.SignLat = pin.pin.Position.Latitude.ToString();
                                _markerDetails.SignLong = pin.pin.Position.Longitude.ToString();
                                _markerDetails.SignImage = pin.pinImage;
                            }
                        }
                    }
                }
            }
                ((CustomMap)this.Element).PinClicked(_markerDetails);
            return true;
        }
        protected override void OnLayout(bool changed, int l, int t, int r, int b)
        {
            base.OnLayout(changed, l, t, r, b);
            if (changed)
            {
                isDrawn = false;
            }
        }

        /// <summary>  
        /// this method will draw polyline on Map  
        /// </summary>  
        private void UpdatePolyLine()
        {
            try
            {
                RemovePlolyline();
                foreach (var routeCordinates in ((CustomMap)this.Element).RouteCoordinates)
                {
                    foreach (var positionEstimate in routeCordinates.PositionEstimateList)
                    {
                        if (routeCordinates.SubmitterName == null)
                            return;
                        var color = routeCordinates.trackColor;
                        if (!string.IsNullOrEmpty(color))
                        {
                            var trackColor = GetColor(color);
                            var polylineOptions = new PolylineOptions();
                            polylineOptions.InvokeColor(trackColor);
                            foreach (var position in positionEstimate.actualPosition)
                            {
                                polylineOptions.Add(new LatLng(position.Latitude, position.Longitude));
                            }
                            polyline = map.AddPolyline(polylineOptions);
                            PolylineBuffer.Add(polyline);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //Log exception to hockey app
            }
        }

        /// <summary>  
        /// here it is restricted to show the 5 tracks only so 5 colors are shown for 5 tracks  
        /// one can easily modify by using random color and showing legends accordingly for tracks.  
        /// </summary>  
        /// <param name="color"></param>  
        /// <returns></returns>  
        private Android.Graphics.Color GetColor(string color)
        {
            Android.Graphics.Color trackColor = Android.Graphics.Color.Orange;
            switch (color)
            {
                case "1":
                    trackColor = Android.Graphics.Color.Rgb(77, 123, 224);
                    break;
                case "2":
                    trackColor = Android.Graphics.Color.Rgb(50, 193, 214);
                    break;
                case "3":
                    trackColor = Android.Graphics.Color.Rgb(163, 178, 78);
                    break;

                case "4":
                    trackColor = Android.Graphics.Color.Rgb(187, 93, 153);
                    break;
                case "5":
                    trackColor = Android.Graphics.Color.Rgb(175, 98, 46);
                    break;
            }
            return trackColor;
        }

        /// <summary>  
        /// Removing multiple polylines at once  
        /// </summary>  
        private void RemovePlolyline()
        {
            foreach (Polyline line in PolylineBuffer)
            {
                line.Remove();
            }
            PolylineBuffer.Clear();
        }
        bool _isReady;
        public async void OnMapReady(GoogleMap googleMap)
        {
            try
            {
                if (_isReady) return;
                _isReady = true;
                map = googleMap;
                var bounds = googleMap.Projection.VisibleRegion;
                if (App.CurrentLat == 0 && App.CurrentLong == 0)
                {
                    var CurrentPosition = await ((CustomMap)this.Element).GetGeolocation();
                    if (CurrentPosition != null)
                    {
                        App.CurrentLat = CurrentPosition.Latitude;
                        App.CurrentLong = CurrentPosition.Longitude;
                        map.MoveCamera(CameraUpdateFactory.NewLatLngZoom(new LatLng(App.CurrentLat, App.CurrentLong),
                            App.CurrentZoomLevel));
                    }
                    else
                    {
                        map.MoveCamera(CameraUpdateFactory.NewLatLngZoom(new LatLng(52.52, 13.40), App.CurrentZoomLevel));
                    }
                }
                else
                {
                    map.MoveCamera(CameraUpdateFactory.NewLatLngZoom(new LatLng(App.CurrentLat, App.CurrentLong), App.CurrentZoomLevel));
                }
                map.CameraChange += Map_CameraChange;
                await Task.Run(() =>
                {
                    if (((CustomMap)this.Element).RouteCoordinates != null)
                    {
                        if (((CustomMap)this.Element).RouteCoordinates.Count > 0)
                        {
                            UpdatePolyLine();
                        }
                    }
                });
                await Task.Run(() =>
                {
                    if (((CustomMap)this.Element).CustomPins != null)
                    {
                        if (((CustomMap)this.Element).CustomPins.Count > 0)
                        {
                            SetMapMarkers();
                        }
                    }
                });
            }
            catch (Exception ex)
            {
                //Log exception to hockey app
            }
        }
    }
}
