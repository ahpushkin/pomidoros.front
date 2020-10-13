using System.Collections.Generic;

namespace Core.Navigation
{
    public class NavigationParameters : Dictionary<string, object>
    {
        public bool TryGetParameter<TParameter>(string key, out TParameter parameter)
        {
            var result = false;
            parameter = default;

            if (ContainsKey(key) && this[key] is TParameter casted)
            {
                result = true;
                parameter = casted;
            }

            return result;
        }
    }
}