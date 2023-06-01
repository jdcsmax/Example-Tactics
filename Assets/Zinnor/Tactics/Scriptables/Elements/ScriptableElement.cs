using UnityEngine;

namespace Zinnor.Tactics.Scriptables.Weapons
{
    /**
     * 元素
     */
    [CreateAssetMenu(fileName = "ScriptableElement", menuName = "ScriptableObjects/ScriptableElement")]
    public class ScriptableElement : ScriptableObject
    {
        /**
         * 描述
         */
        public string Message;
    }
}