using Core.ViewModel.Infra;
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
                ImageSource = "marker_2"
            };
        }

        public static MapItemViewModel CreateEndItem(Position position)
        {
            return new MapItemViewModel
            {
                Position = position,
                Name = "Окончание",
                ImageSource = "marker_1"
            };
        }

        public static MapItemViewModel CreateCourierItem(Position position)
        {
            return new MapItemViewModel
            {
                Position = position,
                Name = "Вы",
                ImageSource = "car"
            };
        }

        public string Name { get; set; }
        public Position Position { get; set; }
        public string ImageSource { get; set; }
    }
}
