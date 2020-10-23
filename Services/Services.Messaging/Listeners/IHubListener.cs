using System.Threading.Tasks;

namespace Services.Messaging.Listeners
{
    public interface IHubListener
    {
        string EventName { get; }

        Task HandleAsync(object parameter);
    }
}