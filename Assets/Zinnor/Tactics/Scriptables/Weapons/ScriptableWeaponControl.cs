using System.Collections.Generic;
using UnityEngine;
using Zinnor.Tactics.Scriptables.Elements;

namespace Zinnor.Tactics.Scriptables.Weapons
{
    /**
     * 武器操控
     */
    [CreateAssetMenu(fileName = "ScriptableWeaponControl", menuName = "ScriptableObjects/ScriptableWeaponControl")]
    public class ScriptableWeaponControl : ScriptableObject
    {
        /**
         * 描述
         */
        public string Message;

        /**
         * 类型
         */
        public ScriptableWeaponType Type;

        /**
         * 元素
         */
        public List<ScriptableElement> Elements;

        /**
         * 等级
         */
        public string Rank;
    }
}