using System.Collections;
using System.Collections.Generic;
using Exiled.API.Features;

namespace CustomizableSpecialRounds.Features.SpecialRounds.Core
{
    public class SpecialRoundParameters : IEnumerable<KeyValuePair<string, object>>
    {
        private readonly Dictionary<string, object> _values = new Dictionary<string, object>();

        public T Get<T>(string key)
        {
            return _values.TryGetValue(key, out var val) ? (T)val : default;
        }

        public void Set(string key, object value)
        {
            _values[key] = value;
            
            Log.Debug($"Successfully setting {key} to {value}.");
        }

        public bool TrySetExisting(string key, object value, out string error)
        {
            if (!_values.ContainsKey(key))
            {
                error = "Parameter not found.";
                Log.Debug($"Error while trying to set {key} to {value}: {error}");
                return false;
            }
            
            Set(key, value);

            error = string.Empty;
            return true;
        }

        public IEnumerator<KeyValuePair<string, object>> GetEnumerator()
        {
            return _values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}