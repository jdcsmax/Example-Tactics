using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Zinnor.Tactics.Scriptables
{
    [CreateAssetMenu(fileName = "ScriptableInt", menuName = "ScriptableObjects/ScriptableInt")]
    public class ScriptableInt : ScriptableObject
    {
        public int Value;
    }
}