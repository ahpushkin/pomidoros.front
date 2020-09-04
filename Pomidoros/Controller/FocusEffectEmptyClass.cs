using System;
using Xamarin.Forms;

namespace Pomidoros.Controller
{
    public class FocusEffectEmptyClass : RoutingEffect
    {
        public FocusEffectEmptyClass() : base($"MyCompany.{nameof(FocusEffect)}")
        {
        }

        private static object FocusEffect()
        {
            throw new NotImplementedException();
        }
    }
}
