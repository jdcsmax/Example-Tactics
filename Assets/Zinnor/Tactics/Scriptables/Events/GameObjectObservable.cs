using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Zinnor.Tactics.Scriptables.Events
{
    [Serializable]
    [CreateAssetMenu(fileName = "GameObjectObservable", menuName = "Events/GameObjectObservable", order = 2)]
    public class GameObjectObservable : ScriptableObservable<GameObject>
    {
        [FormerlySerializedAs("Subject")] [SerializeField]
        private GameObject _subject;

        public override GameObject Subject => _subject;
    }
}