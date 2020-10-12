using Pomidoros.ViewModel.ListElements;
using Services.Models.Enums;
using Xamarin.Forms;

namespace Pomidoros.TemplateSelectors
{
    public class OrderCellDataTemplateSelector : DataTemplateSelector
    {
        public DataTemplate DefaultTemplate { get; set; }
        public DataTemplate TimedTemplate { get; set; }
        public DataTemplate SpecialTemplate { get; set; }

        protected override DataTemplate OnSelectTemplate (object item, BindableObject container)
        {
            var order = item as ShortOrderViewModel;
            switch (order.Type)
            {
                case EOrderType.Default: return DefaultTemplate;
                case EOrderType.Special: return SpecialTemplate;
                case EOrderType.Timed: return TimedTemplate;
                default: return DefaultTemplate;
            }
        }
    }
}