using System;
using System.Threading.Tasks;
using Acr.UserDialogs;
using Autofac;
using Core.Exceptions.Helpers;
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

        public override async void OnAppearing()
        {
            base.OnAppearing();

            var currentUser = CurrentUserDataService.TryGetSavedUserData();
            
            if (currentUser == null)
                using (UserDialogs.Loading(maskType: MaskType.Clear))
                    await FetchUserDataAsync();
            
            Model = currentUser?.Transport?.Model;
            Number = currentUser?.Transport?.Number;
        }
        
        private async Task FetchUserDataAsync()
        {
            try
            {
                await CurrentUserDataService.FetchUserDataAsync();
            }
            catch (Exception e)
            {
                ErrorHandlerHelper.Handle(e);
                ErrorToast();
            }
        }
    }
}