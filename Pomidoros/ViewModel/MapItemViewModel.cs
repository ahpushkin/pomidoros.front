using Core.ViewModel.Infra;
using Pomidoros.Controls;
using Xamarin.Forms.Maps;

namespace Pomidoros.ViewModel
{
    public class MapItemViewModel : BindingObject
    {
        public static MapItemViewModel CreateStartItem(Position position)
        {
            return new MapItemViewModel
            {
                Position = position,
                Name = "Начало",
                MarkerType = MarkerType.Start
            };
        }

        public static MapItemViewModel CreateEndItem(Position position)
        {
            return new MapItemViewModel
            {
                Position = position,
                Name = "Окончание",
                MarkerType = MarkerType.End
            };
        }

        public static MapItemViewModel CreateCourierItem(Position position)
        {
            return new MapItemViewModel
            {
                Position = position,
                Name = "Вы",
                MarkerType = MarkerType.Courier
            };
        }

        public string Name { get; set; }
        public Position Position { get; set; }
        public MarkerType MarkerType { get; set; }
    }
}
