using System.Threading.Tasks;

namespace Pomidoros.View.Profile
{
    public partial class WaitPage
    {
        public WaitPage()
        {
            InitializeComponent();
        }
        
        protected async override void OnAppearing()
        {
            base.OnAppearing();

            await Task.Delay(5000);

            Navigation.InsertPageBefore(new BreakPage(), this);
            await Navigation.PopAsync();
        }
    }
}
