using UnityEngine;
using UnityEngine.Events;

namespace Zinnor.Tactics.Scriptables.Events
{
    public class SerializableObserver : MonoBehaviour
    {
        public ScriptableObservable Observable;

        public UnityEvent Response;

        private void OnEnable()
        {
            if (Observable)
            {
                Observable.Subscribe(this);
            }
        }

        private void OnDisable()
        {
            if (Observable)
            {
                Observable.Unsubscribe(this);
            }
        }

        public void On()
        {
            Response?.Invoke();
        }
    }

    public abstract class SerializableObserver<T> : MonoBehaviour
    {
        protected abstract ScriptableObservable<T> Observable { get; }

        protected abstract UnityEvent<T> Response { get; }

        private void OnEnable()
        {
            if (Observable)
            {
                Observable.Subscribe(this);
            }
        }

        private void OnDisable()
        {
            if (Observable)
            {
                Observable.Unsubscribe(this);
            }
        }

        public void On(T param)
        {
            Response?.Invoke(param);
        }
    }
}