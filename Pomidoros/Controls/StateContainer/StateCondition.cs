using Xamarin.Forms;

namespace Pomidoros.Controls.StateContainer
{
    [ContentProperty("Content")]
    public class StateCondition : Xamarin.Forms.View
    {
        public object State { get; set; }
        public Xamarin.Forms.View Content { get; set; }
    }
}