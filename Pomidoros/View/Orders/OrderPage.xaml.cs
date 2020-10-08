using Pomidoros.View.Notification;
using Pomidoros.View.Stopped;
using Rg.Plugins.Popup.Services;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Pomidoros.View.Orders
{
    public partial class OrderPage
    {
        public ICommand ChangeLocationCommand { get; set; }

        public OrderPage()
        {
            InitializeComponent();
            
            ChangeLocationCommand = new Command(ChangeLocation);
        }
        protected async override void OnAppearing()
        {
            base.OnAppearing();


            await Task.Delay(500000);

            if (activ.IsRunning == true)
            {
                //PopupNavigation.Instance.PushAsync(new StoppedPage());
            }
            else
            {
                DisplayAlert("Произошла ошибка.", "Повторите попытку позже. ", "Хорошо");
            }
            void OperatorEvent(object sender, EventArgs args)
            {
                PopupNavigation.Instance.PushAsync(new StoppedPage());
            }
        }

        void CallEvent(object sender, EventArgs args)
        {
            PopupNavigation.Instance.PushAsync(new CallPopupPage());
        }
        void ChangeLocation()
        {
            Navigation.PushAsync(new ChangeLocationPage());
        }
        void ProblemEvent(object sender, EventArgs args)
        {
            PopupNavigation.Instance.PushAsync(new ProblemPopupPage());
        }
        void ShowMap(object sender, EventArgs args)
        {
            Navigation.PushAsync(new MapPage());
        }
        void MoreDeatil(object sender, EventArgs args)
        {
            Navigation.PushAsync(new MorePage());
        }
        void ComfingEvent(object sender, EventArgs args)
        {
            PopupNavigation.Instance.PushAsync(new СonfirmPopupPage());
        }
        void MessageEvent(object sender, EventArgs args)
        {
            PopupNavigation.Instance.PushAsync(new MessagePopupPage());
        }

        private const int UpperPanBound = 480;
        private const int LowerPanBound = 100;

        private void OnPanUpdated(object sender, PanUpdatedEventArgs e)
        {
            if (e.StatusType == GestureStatus.Started && IsUnderBounds())
            {
                scroll.IsEnabled = false;
            }
            else if (e.StatusType == GestureStatus.Running && IsUnderBounds())
            {
                container.Layout(new Rectangle(
                    container.X,
                    container.Y + e.TotalY,
                    container.Width,
                    container.Height - e.TotalY));
            }
            else if (e.StatusType == GestureStatus.Completed && !IsUnderBounds())
            {
                scroll.IsEnabled = true;
            }
        }

        private bool IsUpperUnderBound()
            => container.Height <= UpperRealBound;
        
        private bool IsLowerUnderBound()
            => container.Height >= LoverRealBound;

        private double UpperRealBound => Device.Info.ScaledScreenSize.Height * 3 / 4;
        private double LoverRealBound => Device.Info.ScaledScreenSize.Height / 10;
        
        private bool IsUnderBounds()
            => container.Height <= UpperPanBound && container.Height >= LowerPanBound;
    }
}
