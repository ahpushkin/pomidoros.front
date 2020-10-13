using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Pomidoros.View.Authorization;
using Pomidoros.View.Base;
using Pomidoros.ViewModel.Authorization;

namespace Pomidoros.View.ReviewSteps
{
    public partial class ReadyToWorkPage : BaseContentPage
    {
        public ReadyToWorkPage()
        {
            InitializeComponent();
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            
            await Task.Delay(3000);

            if (activ.IsRunning)
            {
                Navigation.InsertPageBefore(new MainPage(), Navigation.NavigationStack[0]);
                Navigation.PopToRootAsync();
            }
            else
            {
                DisplayAlert("Произошла ошибка.", "Повторите попытку позже. ", "Хорошо");
            }
        }

    }
 }

