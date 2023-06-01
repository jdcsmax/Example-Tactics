using UnityEngine;

namespace Zinnor.Tactics.Scriptables
{
    [CreateAssetMenu(fileName = "ScriptableBool", menuName = "ScriptableObjects/ScriptableBool")]
    public class ScriptableBool : ScriptableObject
    {
        public bool Value;
    }
}