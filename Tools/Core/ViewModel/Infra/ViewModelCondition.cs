namespace Core.ViewModel.Infra
{
    public class ViewModelCondition : BindingObject
    {
        private bool _isSatisfy;
        public bool IsSatisfy
        {
            get => _isSatisfy;
            set => SetProperty(ref _isSatisfy, value);
        }
    }
}