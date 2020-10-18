using System.Threading.Tasks;
using Autofac;
using Pomidoros.View.ReviewSteps;
using Services.CurrentUser;

namespace Pomidoros.View.Authorization
{
    public partial class WelcomePage
    {
        public WelcomePage()
        {
            InitializeComponent();
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            var userDataProvider = App.Container.Resolve<ICurrentUserDataService>();
            name.Text = userDataProvider.TryGetSavedUserData().FullName;

            await Task.Delay(3000);
            Navigation.InsertPageBefore(new FirstReviewPage(), this);
            await Navigation.PopAsync();
        }
    }
}
