using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Pomidoros.StateContainer
{
    [ContentProperty(nameof(Conditions))]
    public class StateContainer : ContentView
    {
        private List<StateCondition> _conditions { get; } = new List<StateCondition>();
        public IList<StateCondition> Conditions => _conditions;

        public static readonly BindableProperty StateProperty =
            BindableProperty.Create(
                nameof(State),
                typeof(object),
                typeof(StateContainer),
                null,
                BindingMode.Default,
                null,
                StateChanged);

        public object State
        {
            get => GetValue(StateProperty);
            set => SetValue(StateProperty, value);
        }

        private async Task ChooseStateProperty(object newValue)
        {
            if (Conditions == null && Conditions?.Count == 0)
                return;

            try
            {
                var condition = Conditions.First(stateCondition =>
                    stateCondition.State != null &&
                    stateCondition.State.ToString().Equals(newValue.ToString()));
            
                if (Content != null)
                {
                    await Content.FadeTo(0, 100U);
                    Content.IsVisible = false;
                    await Task.Delay(30);
                }
                
                condition.Content.Opacity = 0;
                Content = condition.Content;
                Content.IsVisible = true;
                await Content.FadeTo(1);

            } catch (Exception e)
            {
                Debug.WriteLine($"StateContainer ChooseStateProperty {newValue} error: {e}");
                Debugger.Break();
            }
        }
        
        private static async void StateChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var parent = bindable as StateContainer;
            if (parent != null)
                await parent.ChooseStateProperty(newValue);
        }
    }
}