using System.Threading;
using Autofac;
using Core.Extensions;
using Core.ViewModel.Infra;
using Pomidoros.Constants;
using Pomidoros.View.Profile;
using Pomidoros.ViewModel.Base;
using Services.CurrentUser;

namespace Pomidoros.ViewModel.Profile
{
    public class WaitPageViewModel : BaseStepViewModel, INavigateBack
    {
        readonly CancellationTokenSource _cts = new CancellationTokenSource();

        public WaitPageViewModel()
        {
            Title = "Перерыв";
        }

        protected override string CurrentStep => FlowSteps.Break;

        private ICurrentUserDataService _currentUserDataService;
        protected ICurrentUserDataService CurrentUserDataService
            => _currentUserDataService ??= App.Container.Resolve<ICurrentUserDataService>();

        public void NavigateBack()
        {
            FlowFlagManager.Set(CurrentStep, false);
        }

        public override async void OnAppearing()
        {
            base.OnAppearing();

            if(await CurrentUserDataService.RequestBreakAsync(_cts.Token))
            {
                Navigation.InsertPageBefore(new BreakPage(), Navigation.GetCurrent());
                await Navigation.PopAsync();
            }
        }

        public override void OnDisappearing()
        {
            base.OnDisappearing();

            _cts.Cancel();
        }
    }
}
