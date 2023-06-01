using System.Collections.Generic;
using UnityEngine;
using Zinnor.Tactics.Scriptables.Genders;

namespace Zinnor.Tactics.Scriptables.Weapons
{
    /**
     * 武器
     */
    [CreateAssetMenu(fileName = "ScriptableWeapon", menuName = "ScriptableObjects/ScriptableWeapon")]
    public class ScriptableWeapon : ScriptableObject
    {
        /**
         * 名称
         */
        public string Name;

        /**
         * 类型
         */
        public ScriptableWeaponType Type;


        /**
         * 元素
         */
        public ScriptableElement Element;

        /**
         * 性别
         */
        public ScriptableGender Gender;

        /**
         * 效果
         */
        public List<ScriptableEffect> Effects;

        /**
         * 等级
         */
        public string Rank;

        /**
         * 最小范围
         */
        public int MinRange;

        /**
         * 最大范围
         */
        public int MaxRange;

        /**
         * 耐久
         */
        public int Uses;

        /**
         * 威力
         */
        public int Might;

        /**
         * 命中
         */
        public int Hit;

        /**
         * 暴击
         */
        public int Critical;

        /**
         * 武器经验
         */
        public int WeaponExp;

        /**
         * 角色经验
         */
        public int Exp;

        /**
         * 价格
         */
        public int Price;
    }
}