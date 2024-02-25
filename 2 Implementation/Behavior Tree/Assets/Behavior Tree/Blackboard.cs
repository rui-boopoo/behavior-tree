using System.Collections.Generic;
using UnityEngine;

namespace Boopoo.BehaviorTrees
{
    public class Blackboard : MonoBehaviour
    {
        private readonly Dictionary<string, object> _database = new();

        public T Read<T>(string key)
        {
            if (!_database.TryGetValue(key, out object value))
                throw new KeyNotFoundException($"The key '{key}' was not found.");

            if (value is T typedValue) return typedValue;
            throw new System.InvalidCastException($"The value for key '{key}' is not of type '{typeof(T).Name}'.");
        }

        public void Write(string key, object value)
        {
            if (string.IsNullOrEmpty(key))
                throw new System.ArgumentException("Write to blackboard using a null key", nameof(key));

            _database[key] = value;
        }

        public bool Remove(string key)
        {
            return _database.Remove(key);
        }

        public bool Contains(string key)
        {
            return _database.ContainsKey(key);
        }

        public void Clear()
        {
            _database.Clear();
        }
    }
}