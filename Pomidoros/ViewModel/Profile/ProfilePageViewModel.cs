using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Autofac;
using Core.Commands;
using Core.Exceptions.Helpers;
using Core.Extensions;
using Core.ViewModel.Infra;
using Pomidoros.View.Authorization;
using Pomidoros.ViewModel.Base;
using Services.Authorization;
using Services.CurrentUser;
using Services.Models.Data;
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

        public string FullName => $"{FirstName} {LastName}";

        private string _firstName;
        public string FirstName
        {
            get => _firstName;
            set => SetProperty(ref _firstName, value);
        }

        private string _lastName;
        public string LastName
        {
            get => _lastName;
            set => SetProperty(ref _lastName, value);
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

        private int _ideftify;
        public int Identify
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
            
            if (e.PropertyName == nameof(LastName) || e.PropertyName == nameof(FirstName))
                RaisePropertyChanged(nameof(FullName));
        }

        private async Task SaveUserDataToStorage()
        {
            try
            {
                var userData = CurrentUserDataService.TryGetSavedUserData();
                await CurrentUserDataService.UpdateUserDataAsync(new UserUpdateModel
                {
                    Identify = userData.Identify,
                    FirstName = FirstName,
                    LastName = LastName,
                    Phone = Phone,
                    Email = Email,
                });
                
                Toast("Измененные данные были сохранены");
            }
            catch (Exception e)
            {
                ErrorHandlerHelper.Handle(e);
                ErrorToast();
            }
        }

        private void UpdateUserDataFromStorage()
        {
            var userData = CurrentUserDataService.TryGetSavedUserData();

            if (userData != null)
            {
                FirstName = userData.FirstName;
                LastName = userData.LastName;
                Identify = userData.Identify;
                Phone = userData.Phone;
                Email = userData.Email;
                Transport = userData.Transport?.FirstOrDefault();
                RaisePropertyChanged(nameof(FullName));
            }
        }
        
        private async Task OnLogoutCommandAsync(object obj)
        {
            await AuthorizationService.LogoutAsync();
            Navigation.InsertPageBefore(new LoginPage(), Navigation.GetRoot());
            await Navigation.PopToRootAsync();
        }
    }
}