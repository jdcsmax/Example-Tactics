using System;
using System.Collections.Generic;
using UnityEngine;

namespace Zinnor.Tactics.Scriptables.Events
{
    [Serializable]
    [CreateAssetMenu(fileName = "ScriptableObservable", menuName = "Events/ScriptableObservable", order = 1)]
    public class ScriptableObservable : ScriptableObject
    {
        private readonly List<SerializableObserver> _observers = new();

        public void Subscribe(SerializableObserver observer)
        {
            if (_observers.Contains(observer))
            {
                return;
            }

            _observers.Add(observer);
        }

        public void Unsubscribe(SerializableObserver observer)
        {
            _observers.Remove(observer);
        }

        public void Emit(Action action = null)
        {
            for (var i = _observers.Count - 1; i >= 0; i--)
            {
                _observers[i].On();
            }

            action?.Invoke();
        }
    }

    [Serializable]
    public class ScriptableObservable<T> : ScriptableObject
    {
        private readonly List<SerializableObserver<T>> _observers = new();

        public virtual T Subject => default;

        public virtual void Emit(Action action = null)
        {
            for (var i = _observers.Count - 1; i >= 0; i--)
            {
                _observers[i].On(Subject);
            }

            action?.Invoke();
        }

        public virtual void Emit(T param, Action action = null)
        {
            for (var i = _observers.Count - 1; i >= 0; i--)
            {
                _observers[i].On(param);
            }

            action?.Invoke();
        }

        public void Subscribe(SerializableObserver<T> observer)
        {
            if (_observers.Contains(observer))
            {
                return;
            }

            _observers.Add(observer);
        }

        public void Unsubscribe(SerializableObserver<T> observer)
        {
            _observers.Remove(observer);
        }
    }
}