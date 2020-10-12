using System;
using Acr.UserDialogs;
using Autofac;
using Pomidoros.Controller;
using Pomidoros.Interfaces;
using Pomidoros.View.Base;
using Pomidoros.View.Notification;
using Pomidoros.View.ReviewSteps;
using Rg.Plugins.Popup.Services;
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

            var requestRes = await App.Container.Resolve<IRequestsToServer>()?.GetDriverDataAsync();
            
            if (requestRes)
            {
                Navigation.InsertPageBefore(new FirstReviewPage(), this);
                Navigation.PopAsync();
            }
            else
            {
                UserDialogs.Instance.AlertAsync("Произошла ошибка.", "Не удалось загрузить данные вашего профиля. Повторите попытку позже. ", "Хорошо").SafeFireAndForget(false);
            }            
        }
    }
}
