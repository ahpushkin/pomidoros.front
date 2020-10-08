using System;
using System.Linq;
using System.Threading.Tasks;
using Core.ViewModel.Infra;
using Pomidoros.View.Profile;
using Pomidoros.ViewModel.Base;
using Xamarin.Forms;

namespace Pomidoros.ViewModel.Profile
{
    public class BreakPageViewModel : BaseViewModel, IAppearingAware
    {
        private readonly int goal = 900;
        public string main_data;
        public int count;

        public BreakPageViewModel()
        {
            Title = "Перерыв";
        }
        
        private string _timerProperty;
        public string TimerProperty
        {
            get => _timerProperty;
            set => SetProperty(ref _timerProperty, value);
        }
        
        private Color _baseColor;
        public Color BaseColor
        {
            get => _baseColor;
            set => SetProperty(ref _baseColor, value);
        }
        
        public async void OnAppearing()
        {
            BaseColor = Color.FromHex("#96A637");
            
            count = goal;
            UpdateTime();
            while (true)
            {
                await Task.Delay(1000);
                count--;
                UpdateTime();

                if (count == 0)
                {
                    Navigation.InsertPageBefore(new OverPage(), Navigation.NavigationStack.Last());
                    await Navigation.PopAsync();
                    break;
                }
            }
        }

        private void UpdateTime()
        {
            if (count < 300)
                BaseColor = Color.Red;

            var time = TimeSpan.FromSeconds(count);

            var str = time.ToString(@"mm\:ss");
            main_data = str;
            TimerProperty = main_data;
        }
    }
}