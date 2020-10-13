using Pomidoros.Constants;
using Pomidoros.ViewModel.Base;

namespace Pomidoros.ViewModel.ReviewSteps
{
    public class SecondReviewPageViewModel : BaseStepViewModel
    {
        protected override string CurrentStep => FlowSteps.CheckUniformStep;
    }
}