using UnityEngine;

namespace Zinnor.Tactics.Scriptables.Genders
{
    /**
     * 性别
     */
    [CreateAssetMenu(fileName = "ScriptableGender", menuName = "ScriptableObjects/ScriptableGender")]
    public class ScriptableGender : ScriptableObject
    {
        /**
         * 描述
         */
        public string Message;
    }
}