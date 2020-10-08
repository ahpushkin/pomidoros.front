using System;

namespace Pomidoros.View.Profile
{
    public partial class OverPage
    {
        public OverPage()
        {
            InitializeComponent();
        }
        
        void MainEvent(object sender, EventArgs args)
        {
            Navigation.PopToRootAsync();
        }
    }
}
