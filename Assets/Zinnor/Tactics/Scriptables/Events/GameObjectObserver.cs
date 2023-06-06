using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace Zinnor.Tactics.Scriptables.Events
{
    public class GameObjectObserver : SerializableObserver<GameObject>
    {
        [FormerlySerializedAs("Observable")] [SerializeField]
        private GameObjectObservable _observable;

        [FormerlySerializedAs("Response")] [SerializeField]
        private UnityEvent<GameObject> _response;

        protected override ScriptableObservable<GameObject> Observable => _observable;
        protected override UnityEvent<GameObject> Response => _response;
    }
}