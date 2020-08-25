
# pom_frontend

<img src="https://github.com/ahpushkin/pomidoros.front/blob/master/Screenshots/example.png" />

## Google Maps API key (Android)
For Android, you'll need to obtain a Google Maps API key:
https://developer.xamarin.com/guides/android/platform_features/maps_and_location/maps/obtaining_a_google_maps_api_key/

Insert it in `~/Droid/Properties/AndroidManifest.xml`:

    <application android:label="Customers" android:theme="@style/CustomersTheme">\
      ...
      <meta-data android:name="com.google.android.geo.API_KEY" android:value="[YOUR API KEY HERE]" />
      ...
    </application>

## Licenses

This project uses some third-party assets with a license that requires attribution:

- Montserrat
- [SkiaSharp](https://www.nuget.org/packages/SkiaSharp)
- [FFImageLoading](https://github.com/daniel-luberda/FFImageLoading)
- [Skor.Controls](https://github.com/skordesign/SKOR.UI)
- [Xamarin.Forms.GoogleMaps](https://github.com/xamarin/googlemaps)
