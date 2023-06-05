using System.Collections.Generic;
using UnityEngine;
using Zinnor.Tactics.Scriptables.Effects;
using Zinnor.Tactics.Scriptables.Restrictions;
using Zinnor.Tactics.Scriptables.Weapons;

namespace Zinnor.Tactics.Scriptables.Classes
{
    /// <summary>
    /// 职业
    /// </summary>
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

        /// <summary>
        /// 力量
        /// </summary>
        public int Strength;

        /// <summary>
        /// 魔力
        /// </summary>
        public int Magic;

        /// <summary>
        /// 技巧
        /// </summary>
        public int Skill;

        /// <summary>
        /// 速度
        /// </summary>
        public int Speed;

        /// <summary>
        /// 幸运
        /// </summary>
        public int Luck;

        /// <summary>
        /// 物防
        /// </summary>
        public int Defense;

        /// <summary>
        /// 魔防
        /// </summary>
        public int Resistance;

        /// <summary>
        /// 移动
        /// </summary>
        public int Movement;

        /// <summary>
        /// 飞行
        /// </summary>
        public bool Flying;

        /// <summary>
        /// 骑兵
        /// </summary>
        public bool Horseback;

        /// <summary>
        /// 重甲
        /// </summary>
        public bool Armored;

        /// <summary>
        /// 魔法
        /// </summary>
        public bool Magical;

        /// <summary>
        /// 龙
        /// </summary>
        public bool Dragon;

        /// <summary>
        /// 武器列表
        /// </summary>
        public List<ScriptableWeaponControl> Controls;

        /// <summary>
        /// 能力列表
        /// </summary>
        public List<ScriptableEffect> Abilities;

        /// <summary>
        /// 限制列表
        /// </summary>
        public List<ScriptableRestriction> Restrictions;
    }
}