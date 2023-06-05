using UnityEngine;

namespace Zinnor.Tactics.Scriptables.Weapons
{
    /**
     * 武器类型
     */
    [CreateAssetMenu(fileName = "ScriptableWeaponType", menuName = "ScriptableObjects/ScriptableWeaponType")]
    public class ScriptableWeaponType : ScriptableObject
    {
        /// <summary>
        /// 描述信息
        /// </summary>
        public string Message;
    }
}