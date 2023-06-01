using UnityEngine;

namespace Zinnor.Tactics.Scriptables.Weapons
{
    /**
     * 武器类型
     */
    [CreateAssetMenu(fileName = "ScriptableWeaponType", menuName = "ScriptableObjects/ScriptableWeaponType")]
    public class ScriptableWeaponType : ScriptableObject
    {
        /**
         * 描述
         */
        public string Message;
    }
}