using System;
using Xamarin.Forms;

namespace Pomidoros.Model
{
    public class Countdown : BindableObject
    {
        //CounDown calss for progres bar
        //timer
        TimeSpan _remainTime;

        public event Action Completed;
        public event Action Ticked;

        public DateTime EndDate { get; set; }

        public TimeSpan RemainTime
        {
            get { return _remainTime; }

            private set
            {
                _remainTime = value;
                OnPropertyChanged();
            }
        }
        //start method
        public void Start(int seconds = 1)
        {
            Device.StartTimer(TimeSpan.FromSeconds(seconds), () =>
            {
                RemainTime = (EndDate - DateTime.Now);

                var ticked = RemainTime.TotalSeconds > 1;

                if (ticked)
                {
                    Ticked?.Invoke();
                }
                else
                {
                    Completed?.Invoke();
                }

                return ticked;
            });
        }
        //stop method not needed
    }
}
