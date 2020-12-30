using System.Threading.Tasks;
using Autofac;
using Pomidoros.View.Base;
using Services.CurrentUser;

namespace Pomidoros.View.Authorization
{
    public partial class WelcomePage : BaseContentPage
    {
        public WelcomePage()
        {
            InitializeComponent();
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            var userDataProvider = App.Container.Resolve<ICurrentUserDataService>();
            name.Text = userDataProvider.GetUserData().FullName;

            await Task.Delay(2000);

            Navigation.InsertPageBefore(new ReviewSteps.FirstReviewPage(), this);
            await Navigation.PopAsync();
        }
    }
}
