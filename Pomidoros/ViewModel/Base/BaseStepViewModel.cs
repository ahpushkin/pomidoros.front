using Autofac;
using Core.ViewModel.Infra;
using Pomidoros.Constants;
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
        }

        protected void SetFlowFlag()
        {
            FlowFlagManager.Set(CurrentStep, true);
        }
    }
}