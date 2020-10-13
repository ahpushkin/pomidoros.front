using Core.ViewModel.Infra;
using Pomidoros.Constants;
using Pomidoros.ViewModel.Base;

namespace Pomidoros.ViewModel.Profile
{
    public class OverPageViewModel : BaseStepViewModel, INavigateBack
    {
        public OverPageViewModel()
        {
            Title = "Перерыв";
        }
        
        protected override string CurrentStep => FlowSteps.Break;

        public void NavigateBack()
        {
            FlowFlagManager.Set(CurrentStep, false);
        }
    }
}