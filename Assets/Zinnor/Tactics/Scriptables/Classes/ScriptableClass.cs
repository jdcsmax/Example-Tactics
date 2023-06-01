using System.Collections.Generic;
using UnityEngine;
using Zinnor.Tactics.Scriptables;
using Zinnor.Tactics.Scriptables.Weapons;

namespace Zinnor.Tactics.Units
{
    /**
     * 职业
     */
    [CreateAssetMenu(fileName = "ScriptableClass", menuName = "ScriptableObjects/ScriptableClass")]
    public class ScriptableClass : ScriptableObject
    {
        /**
         * 描述
         */
        public string Message;

        /**
         * 生命
         */
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