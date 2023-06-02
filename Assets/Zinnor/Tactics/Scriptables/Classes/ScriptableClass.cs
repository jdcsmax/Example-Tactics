using System.Collections.Generic;
using CollisionBear.PreviewObjectPicker;
using UnityEngine;
using Zinnor.Tactics.Scriptables.Effects;
using Zinnor.Tactics.Scriptables.Weapons;

namespace Zinnor.Tactics.Scriptables.Classes
{
    /**
     * 职业
     */
    [CreateAssetMenu(fileName = "ScriptableClass", menuName = "ScriptableObjects/ScriptableClass")]
    public class ScriptableClass : ScriptableObject
    {
        /// <summary>
        /// 描述
        /// </summary>
        public string Message;

        /// <summary>
        /// 生命
        /// </summary>
        public int HP;

        /**
         * 力量
         */
        public int Strength;

        /**
         * 魔力
         */
        public int Magic;

        /**
         * 技巧
         */
        public int Skill;

        /**
         * 速度
         */
        public int Speed;

        /**
         * 幸运
         */
        public int Luck;

        /**
         * 物防
         */
        public int Defense;

        /**
         * 魔防
         */
        public int Resistance;

        /**
         * 移动
         */
        public int Movement;

        /**
         * 飞行单位
         */
        public bool Flying;

        /**
         * 骑兵单位
         */
        public bool Horseback;

        /**
         * 重甲单位
         */
        public bool Armored;

        /**
         * 魔法单位
         */
        public bool Magical;

        /**
         * 龙单位
         */
        public bool Dragon;

        /**
         * 武器列表
         */
        public List<ScriptableWeaponControl> Controls;

        /**
         * 能力列表
         */
        public List<ScriptableEffect> Abilities;
    }
}