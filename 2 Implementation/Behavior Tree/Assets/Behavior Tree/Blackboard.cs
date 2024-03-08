using System;
using System.Collections.Generic;
using UnityEngine;

namespace Boopoo.BehaviorTrees
{
    public class Blackboard : MonoBehaviour
    {
        private readonly Dictionary<string, object> _database = new();

        /// <summary>
        /// Reads a value of type T from the blackboard using the specified key.
        /// </summary>
        /// <typeparam name="T">The expected type of the value to be read.</typeparam>
        /// <param name="key">The key associated with the value to be read.</param>
        /// <returns>The value associated with the specified key, cast to type T.</returns>
        /// <exception cref="KeyNotFoundException">Thrown if the key is not found in the database.</exception>
        /// <exception cref="InvalidCastException">Thrown if the value cannot be cast to type T.</exception>
        public T Read<T>(string key)
        {
            // Attempt to get the value associated with the key.
            if (!_database.TryGetValue(key, out object value))
            {
                // Key not found, throw an exception.
                throw new KeyNotFoundException($"The key '{key}' was not found.");
            }

            // If the value is null, return the default for the type T.
            // Note: This returns null for reference types and zeroes for numeric value types.
            if (value == null) return default(T);

            // Attempt to cast the value to the requested type T.
            if (value is T typedValue)
            {
                // Successful cast, return the typed value.
                return typedValue;
            }

            // The value cannot be cast to type T, throw an exception.
            throw new InvalidCastException(
                $"The value for key '{key}' is not of the expected type '{typeof(T).Name}'; actual type is '{value.GetType().Name}'.");
        }

        /// <summary>
        /// Tries to read a value of type T from the blackboard using the specified key, returning a boolean indicating success or failure.
        /// </summary>
        /// <typeparam name="T">The expected type of the value to be read.</typeparam>
        /// <param name="key">The key associated with the value to be read.</param>
        /// <param name="value">The out parameter that will contain the value if the read is successful.</param>
        /// <returns>True if the value was successfully read and matched the type T, otherwise false.</returns>
        public bool TryRead<T>(string key, out T value)
        {
            // Attempt to get the value associated with the key.
            if (_database.TryGetValue(key, out object rawValue))
            {
                // If the value is of type T, set the out parameter and return true.
                if (rawValue is T typedValue)
                {
                    value = typedValue;
                    return true;
                }
            }

            // If the key is not found or the value is not of type T, set the out parameter to the default value of type T and return false.
            value = default;
            return false;
        }

        public void Write(string key, object value)
        {
            if (string.IsNullOrEmpty(key))
                throw new ArgumentException("Register to blackboard using a null key", nameof(key));

            _database[key] = value;
        }

        public void Register<T>(string key)
        {
            _database[key] = default(T);
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

        public void Print()
        {
            foreach (string key in _database.Keys) Debug.Log($"Key: {key}, Value: {_database[key]}");
        }
    }
}