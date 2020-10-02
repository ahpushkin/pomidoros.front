using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Pomidoros.View.Authorization;
using Pomidoros.View.Base;

namespace Pomidoros.View.ReviewSteps
{
    public partial class ReadyToWorkPage : BaseContentPage
    {
        public ReadyToWorkPage()
        {
            InitializeComponent();
        }

        public Dictionary<string, string> user_data = LoginPage.user_data;

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            var jsondata = JsonConvert.SerializeObject(user_data);

            string path = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            string filename = Path.Combine(path, "userdata.txt");

            using (var streamWriter = new StreamWriter(filename, true))
            {
                streamWriter.WriteLine(jsondata);
            }

            using (var streamReader = new StreamReader(filename))
            {
                string content = streamReader.ReadToEnd();
                System.Diagnostics.Debug.WriteLine(content);
            }

            await Task.Delay(5000);

            if (activ.IsRunning)
            {
                Navigation.InsertPageBefore(new MainPage(), this);
                Navigation.PopAsync();
            }
            else
            {
                DisplayAlert("Произошла ошибка.", "Повторите попытку позже. ", "Хорошо");
            }
        }

    }
 }

