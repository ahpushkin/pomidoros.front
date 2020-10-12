using Autofac;
using Pomidoros.Constants;
using Pomidoros.ViewModel.Base;
using Services.CurrentUser;

namespace Pomidoros.ViewModel.ReviewSteps
{
    public class FirstReviewPageViewModel : BaseStepViewModel
    {
        private ICurrentUserDataService _currentUserDataService;
        protected ICurrentUserDataService CurrentUserDataService
            => _currentUserDataService ??= App.Container.Resolve<ICurrentUserDataService>();

        protected override string CurrentStep => FlowSteps.CheckAutoStep;

        private string _model;
        public string Model
        {
            get => _model;
            set => SetProperty(ref _model, value);
        }

        private string _number;
        public string Number
        {
            get => _number;
            set => SetProperty(ref _number, value);
        }

        public override void OnAppearing()
        {
            base.OnAppearing();

            var currentUser = CurrentUserDataService.GetUserData();
            Model = currentUser.Transport.Model;
            Number = currentUser.Transport.Number;
        }
    }
}