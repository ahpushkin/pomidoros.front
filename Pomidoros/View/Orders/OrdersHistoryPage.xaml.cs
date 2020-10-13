using System;
using Xamarin.Forms;

namespace Pomidoros.View.Orders
{
    public partial class OrdersHistoryPage
    {
        public OrdersHistoryPage()
        {
            InitializeComponent();
            states.State = HistoryOrdersStates.NotDone;
        }
        
        void GetOrder(object sender, EventArgs args)
        {
            Navigation.PushAsync(new OrderPage());
        }
        
        void YesEvent(object sender, EventArgs args)
        {
            yes.BackgroundColor = Color.FromHex("#96A637");
            yes.TextColor = Color.Black;

            no.BackgroundColor = Color.FromHex("#FAFAFA");
            no.TextColor = Color.Black;
            
            states.State = HistoryOrdersStates.NotDone;
        }
        
        void NopeEvent(object sender, EventArgs args)
        {
            no.BackgroundColor = Color.FromHex("#96A637");
            no.TextColor = Color.Black;

            yes.BackgroundColor = Color.FromHex("#FAFAFA");
            yes.TextColor = Color.Black;
            
            states.State = HistoryOrdersStates.Done;
        }
    }
    
    public class HistoryOrdersStates
    {
        public const string Done = nameof(Done);
        public const string NotDone = nameof(NotDone);
    }
}
