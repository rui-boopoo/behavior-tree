using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

namespace Boopoo.BehaviorTrees.Tests
{
    public class BlackboardTests
    {
        private GameObject _gameObject = new();
        private Blackboard _blackboard;
        private string _key = "velocity";
        private float _value = 1.0f;

        [SetUp]
        public void SetUp()
        {
            _gameObject = new GameObject("Test Object");
            _blackboard = _gameObject.AddComponent<Blackboard>();
        }

        [TearDown]
        public void TearDown()
        {
            Object.DestroyImmediate(_gameObject);
        }

        [Test]
        public void AddValue_KeyExistsAfterAddition_ReturnsTrue()
        {
            _blackboard.Write(_key, _value);
            Assert.IsTrue(_blackboard.Contains(_key));
        }

        [Test]
        public void AddValue_KeyAlreadyExists_UpdatesValue()
        {
            _blackboard.Write(_key, _value);
            float newValue = 2.5f;
            _blackboard.Write(_key, newValue); // ³¢ÊÔ¸üÐÂÖµ
            Assert.AreEqual(newValue, _blackboard.Read<float>(_key));
        }

        [Test]
        public void ReadValue_ExistingKey_ReturnsCorrectValue()
        {
            _blackboard.Write(_key, _value);
            float value = _blackboard.Read<float>(_key);
            Assert.AreEqual(_value, value);
        }

        [Test]
        public void ReadValue_NonExistingKey_ThrowsKeyNotFoundException()
        {
            Assert.Throws<KeyNotFoundException>(() => _blackboard.Read<float>("nonExistingKey"));
        }

        [Test]
        public void ReadValue_TypeMismatch_ThrowsInvalidCastException()
        {
            _blackboard.Write(_key, _value);
            Assert.Throws<System.InvalidCastException>(() => _blackboard.Read<int>(_key));
        }

        [Test]
        public void WriteValue_UpdateExistingValue_ReadReturnsNewValue()
        {
            _blackboard.Write(_key, _value);
            float newValue = 2.0f;
            _blackboard.Write(_key, newValue);
            float actualValue = _blackboard.Read<float>(_key);
            Assert.AreEqual(newValue, actualValue);
        }

        [Test]
        public void WriteValue_NullKey_ThrowsArgumentException()
        {
            Assert.Throws<System.ArgumentException>(() => _blackboard.Write(null, _value));
        }

        [Test]
        public void WriteValue_EmptyKey_ThrowsArgumentException()
        {
            Assert.Throws<System.ArgumentException>(() => _blackboard.Write("", _value));
        }

        [Test]
        public void WriteValue_UpdateValueToDifferentType_ReadReturnsUpdatedType()
        {
            _blackboard.Write(_key, _value);
            string newValue = "new value";
            _blackboard.Write(_key, newValue);
            var actualValue = _blackboard.Read<string>(_key);
            Assert.AreEqual(newValue, actualValue);
        }

        [Test]
        public void RemoveValue_KeyRemoved_ReturnsFalse()
        {
            _blackboard.Write(_key, _value);
            _blackboard.Remove(_key);
            Assert.IsFalse(_blackboard.Contains(_key));
        }
    }
}