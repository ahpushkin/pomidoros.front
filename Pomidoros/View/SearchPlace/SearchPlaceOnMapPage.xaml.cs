using Xamarin.Forms;

namespace Pomidoros.View.SearchPlace
{
    public partial class SearchPlaceOnMapPage
    {
        public SearchPlaceOnMapPage()
        {
            InitializeComponent();
        }
        
        private double LoverRealBound => Device.Info.ScaledScreenSize.Height * 3 / 4 - 40;
        private double UpperRealBound => Device.Info.ScaledScreenSize.Height / 10;

        private void OnPanUpdated(object sender, PanUpdatedEventArgs e)
        {
            if (e.StatusType == GestureStatus.Running)
            {
                var dimmenssionY = e.TotalY;
                
                if (dimmenssionY > 0 && IsLowerUnderBound() || dimmenssionY < 0 && IsUpperUnderBound())
                {
                    if (!IsUnderLowerBound(dimmenssionY + container.Y))
                        dimmenssionY = -(container.Y - LoverRealBound) + 1;
                    
                    if (!IsUnderUpperBound(dimmenssionY + container.Y))
                        dimmenssionY = -(container.Y - UpperRealBound) - 1;
                    
                    container.Layout(new Rectangle(
                        container.X,
                        container.Y + dimmenssionY,
                        container.Width,
                        container.Height - dimmenssionY));
                }
            }
        }

        private bool IsUnderLowerBound(double dimm)
            => dimm < LoverRealBound;
        
        private bool IsLowerUnderBound()
            => IsUnderLowerBound(container.Y);

        private bool IsUnderUpperBound(double dimm)
            => dimm > UpperRealBound;

        private bool IsUpperUnderBound()
            => IsUnderUpperBound(container.Y);
    }
}
