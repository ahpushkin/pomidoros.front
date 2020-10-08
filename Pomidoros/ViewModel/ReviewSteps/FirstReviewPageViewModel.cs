using Pomidoros.Constants;
using Pomidoros.ViewModel.Base;

namespace Pomidoros.ViewModel.ReviewSteps
{
    public class FirstReviewPageViewModel : BaseStepViewModel
    {
        protected override string CurrentStep => FlowSteps.CheckAutoStep;
    }
}