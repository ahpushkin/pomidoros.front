using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Services.Messaging.Listeners
{
    public abstract class BaseHubListener<TModel> : IHubListener
    {
        public abstract string EventName { get; }
        
        public Task HandleAsync(object parameter)
        {
            var json = System.Text.Json.JsonSerializer.Serialize(parameter, typeof(object));
            var model = JsonConvert.DeserializeObject<TModel>(json);

            return HandleInternalAsync(model);
        }

        protected abstract Task HandleInternalAsync(TModel model);
    }
}