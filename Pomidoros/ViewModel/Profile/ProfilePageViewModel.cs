using System;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Autofac;
using Core.Commands;
using Core.Extensions;
using Core.ViewModel.Infra;
using Pomidoros.View.Authorization;
using Pomidoros.ViewModel.Base;
using Services.Authorization;
using Services.CurrentUser;
using Services.Models.Data;
using Services.Models.Enums;
using Services.Models.User;

namespace Pomidoros.ViewModel.Profile
{
    public class ProfilePageViewModel : BaseViewModel, INavigateBackAsync, IAppearingAware
    {
        private bool _dataChanged;
        
        #region services

        private IAuthorizationService _authorizationService;
        protected IAuthorizationService AuthorizationService
            => _authorizationService ??= App.Container.Resolve<IAuthorizationService>();

        private ICurrentUserDataService _currentUserDataService;
        protected ICurrentUserDataService CurrentUserDataService
            => _currentUserDataService ??= App.Container.Resolve<ICurrentUserDataService>();

        #endregion
        
        public ProfilePageViewModel()
        {
            Title = "Профиль";
        }
        
        #region properties

        private string _fullName;
        public string FullName
        {
            get => _fullName;
            set => SetProperty(ref _fullName, value);
        }

        private string _phone;
        public string Phone
        {
            get => _phone;
            set => SetProperty(ref _phone, value);
        }

        private string _email;
        public string Email
        {
            get => _email;
            set => SetProperty(ref _email, value);
        }

        private string _ideftify;
        public string Identify
        {
            get => _ideftify;
            set => SetProperty(ref _ideftify, value);
        }

        private TransportModel _transport;
        public TransportModel Transport
        {
            get => _transport;
            set => SetProperty(ref _transport, value);
        }
        
        public ICommand LogoutCommand => new AsyncCommand(OnLogoutCommandAsync);

        #endregion

        public Task NavigateBackAsync()
        {
            if (_dataChanged)
                return SaveUserDataToStorage();
            return Task.CompletedTask;
        }

        public void OnAppearing()
        {
            UpdateUserDataFromStorage();
            PropertyChanged += OnPropertyChanged;
        }

        private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            _dataChanged = true;
        }

        private async Task SaveUserDataToStorage()
        {
            try
            {
                await CurrentUserDataService.UpdateUserDataAsync(new UserDataModel
                {
                    FullName = FullName,
                    Identify = Identify,
                    Email = Email,
                    Phone = Phone,
                    Transport = Transport
                });
                
                Toast("Измененные данные были сохранены");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                ErrorToast();
            }
        }

        private void UpdateUserDataFromStorage()
        {
            var userData = CurrentUserDataService.GetUserData();
            
            FullName = userData.FullName;
            Identify = userData.Identify;
            Email = userData.Email;
            Phone = userData.Phone;
            Transport = new TransportModel
            {
                Type = userData.Transport?.Type ?? ETransportType.OnFoot,
                Model = userData.Transport?.Model,
                Number = userData.Transport?.Number
            };
        }
        
        private Task OnLogoutCommandAsync(object obj)
        {
            AuthorizationService.Logout();
            Navigation.InsertPageBefore(new LoginPage(), Navigation.GetRoot());
            return Navigation.PopToRootAsync();
        }
    }
}