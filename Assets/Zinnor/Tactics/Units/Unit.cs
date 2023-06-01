using System;
using UnityEngine;
using Zinnor.Tactics.Scriptables.Weapons;
using Zinnor.Tactics.Tiles;

namespace Zinnor.Tactics.Units
{
    public class Unit : MonoBehaviour
    {
        /**
         * 单位职业
         */
        public ScriptableClass Class;

        /**
         * 额外状态
         */
        public Stats Stats;

        /**
         * 装备武器
         */
        public ScriptableWeapon Weapon;

        /**
         * 最大生命
         */
        public int MaxHP => Class.HP + Stats.HP;

        /**
         * 当前生命
         */
        public int HP = 0;

        /**
         * 力量
         */
        public int Str => Class.Strength + Stats.Strength;

        /**
         * 魔力
         */
        public int Mag => Class.Magic + Stats.Magic;

        /**
         * 技巧
         */
        public int Skl => Class.Skill + Stats.Skill;

        /**
         * 速度
         */
        public int Spd => Class.Speed + Stats.Speed;

        /**
         * 幸运
         */
        public int Lck => Class.Luck + Stats.Luck;

        /**
         * 物防
         */
        public int Def => Class.Defense + Stats.Defense;

        /**
         * 魔防
         */
        public int Res => Class.Resistance + Stats.Resistance;

        /**
         * 移动
         */
        public int Mov => Class.Movement + Stats.Movement;

        public bool Flying => Class.Flying;
        public bool Horseback => Class.Horseback;
        public bool Armored => Class.Armored;

        /**
         * 最小攻击距离
         *
         * todo: 根据当前使用的武器决定距离
         */
        public int MinAttackDistance => Weapon.MinRange;

        /**
         * 最大攻击距离
         *
         * todo: 根据当前使用的武器决定距离
         */
        public int MaxAttackDistance => Weapon.MaxRange;

        /**
         * 能否通过格子
         */
        public virtual bool Traverable(TileOverlay tile)
        {
            if (tile.Data.Walkable)
            {
                return Traverable(tile.Unit);
            }

            if (tile.Data.Flyable && Flying)
            {
                return true;
            }

            return false;
        }

        /**
         * 可否通过单位
         */
        public virtual bool Traverable(Unit unit)
        {
            if (unit == null)
            {
                return true;
            }

            // todo: 检查是否盟友
            return true;
        }

        /**
         * 移动消耗
         */
        public virtual int MoveCost(TileOverlay tile)
        {
            // todo: 根据地形和职业计算移动消耗
            return 1;
        }

        public virtual void ShowMoveSprite(TileOverlay tile)
        {
            // todo: 放入通过派送类进行覆盖
            // if (Class.name == "Cleric" || Class.name == "Curate")
            // {
            //     if (Weapon != null && Weapon.Type.name == "Staves")
            //     {
            //         tile.ShowHealSprite();
            //         return;
            //     }
            // }

            tile.ShowMoveSprite();
        }

        public virtual void ShowAttackSprite(TileOverlay tile)
        {
            if (Weapon == null)
            {
                tile.HideOverlaySprite();
            }

            // todo: 放入通过派送类进行覆盖
            if (Class.name == "Cleric" || Class.name == "Curate")
            {
                if (Weapon != null && Weapon.Type.name == "Staves")
                {
                    tile.ShowHealSprite();
                    return;
                }
            }

            tile.ShowAttackSprite();
        }
    }
}