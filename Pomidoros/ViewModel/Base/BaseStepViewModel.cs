using Autofac;
using Pomidoros.Constants;
using Pomidoros.ViewModel.Infra;
using Services.FlowFlags;

namespace Pomidoros.ViewModel.Base
{
    public class BaseStepViewModel : BaseViewModel, IAppearingAware, IDisappearingAware
    {
        #region services
        
        private IFlowFlagManager _flowFlagManager;
        protected IFlowFlagManager FlowFlagManager
            => _flowFlagManager ??= App.Container.Resolve<IFlowFlagManager>();

        #endregion
        
        protected virtual string CurrentStep => FlowSteps.MainPageStep;
        
        public virtual void OnAppearing()
        {
            SetFlowFlag();
        }

        public virtual void OnDisappearing()
        {
            RemoveFlowFlag();
        }

        protected virtual void SetFlowFlag()
        {
            FlowFlagManager.Set(CurrentStep, true);
        }

        protected virtual void RemoveFlowFlag()
        {
            FlowFlagManager.Set(CurrentStep, false);
        }
    }
}