using System.Linq;
using log4net;
using UnityEngine;
using Zinnor.Tactics.Scriptables.Abilities;
using Zinnor.Tactics.Scriptables.Classes;
using Zinnor.Tactics.Scriptables.Weapons;
using Zinnor.Tactics.Tiles;

namespace Zinnor.Tactics.Units
{
    public class Unit : MonoBehaviour
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(Unit));

        /// <summary>
        /// 唯一编号
        /// </summary>
        public int Id;

        /// <summary>
        /// 阵营
        /// </summary>
        public int Faction;

        /// <summary>
        /// 职业
        /// </summary>
        public ScriptableClass Class;

        /// <summary>
        /// 武器
        /// </summary>
        public ScriptableWeapon Weapon;

        /// <summary>
        /// 状态
        /// </summary>
        public Stats Extra;

        /// <summary>
        /// 占据格子
        /// </summary>
        public TileOverlay Tile;

        /// <summary>
        /// 最大生命
        /// </summary>
        public int MaxHP => Class.HP + Extra.HP;

        /// <summary>
        /// 当前生命
        /// </summary>
        public int HP = 0;

        /// <summary>
        /// 力量
        /// </summary>
        public int Str => Class.Strength + Extra.Strength;

        /// <summary>
        /// 魔力
        /// </summary>
        public int Mag => Class.Magic + Extra.Magic;

        /// <summary>
        /// 技巧
        /// </summary>
        public int Skl => Class.Skill + Extra.Skill;

        /// <summary>
        /// 速度
        /// </summary>
        public int Spd => Class.Speed + Extra.Speed;

        /// <summary>
        /// 幸运
        /// </summary>
        public int Lck => Class.Luck + Extra.Luck;

        /// <summary>
        /// 物防
        /// </summary>
        public int Def => Class.Defense + Extra.Defense;

        /// <summary>
        /// 魔防
        /// </summary>
        public int Res => Class.Resistance + Extra.Resistance;

        /// <summary>
        /// 移动
        /// </summary>
        public int Mov => Class.Movement + Extra.Movement;

        public bool Flying => Class.Flying;

        public bool Horseback => Class.Horseback;
        public bool Armored => Class.Armored;

        /// <summary>
        /// 最小攻击范围
        /// </summary>
        public int MinAttackRange => Weapon.MinRange;

        /// <summary>
        /// 最大攻击范围
        /// </summary>
        public int MaxAttackRange => Weapon.MaxRange;

        /// <summary>
        /// 单位状态
        /// </summary>
        public UnitStates State;

        /// <summary>
        /// 单位动画器
        /// </summary>
        public UnitAnimator Animator;

        /// <summary>
        /// 精灵绘制器
        /// </summary>
        public SpriteRenderer Renderer;

        public void AfterControllerChanged()
        {
            Animator.AfterControllerChanged();
        }

        public void AfterStateChanged()
        {
            Animator.AfterStateChanged();
        }

        public void AfterPropertySet()
        {
            Animator.AfterPropertySet();
        }

        /// <summary>
        /// 占据格子
        /// </summary>
        public void AcquireTile(TileOverlay tile)
        {
            if (tile == null)
            {
                return;
            }

            Tile = tile;
            tile.Occupier = this;
            transform.position = Tile.transform.position;
        }

        /// <summary>
        /// 释放格子
        /// </summary>
        public void ReleaseTile()
        {
            if (Tile == null)
            {
                return;
            }

            Tile.Occupier = null;
            Tile = null;
        }

        /// <summary>
        /// 能否通过
        /// </summary>
        public bool Traverable(TileOverlay tile)
        {
            if (tile.Walkable)
            {
                return Traverable(tile.Occupier);
            }

            if (tile.Flyable && Flying)
            {
                return Traverable(tile.Occupier);
            }

            return false;
        }

        /// <summary>
        /// 能否通过
        /// </summary>
        private bool Traverable(Unit unit)
        {
            return unit == null || Faction == unit.Faction;
        }

        /// <summary>
        /// 移动消耗
        /// </summary>
        public int MoveCost(TileOverlay tile)
        {
            var restriction = Class.Restrictions.FirstOrDefault(
                r => tile.OverlayData == r.OverlayData);
            return restriction != null ? restriction.MoveCost : 1;
        }

        public static Builder newBuilder()
        {
            return new Builder();
        }

        public sealed class Builder
        {
            private int _Id;
            private int _faction;
            private Unit _prefab;
            private GameObject _parent;
            private ScriptableWeapon _weapon;
            private Stats _extra;
            private TileOverlay _tile;
            private int _sortingOrder;

            public Builder SetId(int id)
            {
                _Id = id;
                return this;
            }

            public Builder SetFaction(int faction)
            {
                _faction = faction;
                return this;
            }

            public Builder SetPrefab(Unit prefab)
            {
                _prefab = prefab;
                return this;
            }

            public Builder SetParent(GameObject parent)
            {
                _parent = parent;
                return this;
            }

            public Builder SetWeapon(ScriptableWeapon weapon)
            {
                _weapon = weapon;
                return this;
            }

            public Builder SetExtra(Stats extra)
            {
                _extra = extra;
                return this;
            }

            public Builder SetTile(TileOverlay tileOverlay)
            {
                _tile = tileOverlay;
                return this;
            }

            public Builder SetSortingOrder(int sortingOrder)
            {
                _sortingOrder = sortingOrder;
                return this;
            }

            public Unit Build()
            {
                var unit = Instantiate(_prefab, _parent.transform);

                unit.Id = _Id;
                unit.Faction = _faction;
                unit.Weapon = _weapon;

                unit.Extra = _extra;
                unit.HP = unit.MaxHP;
                unit.Renderer.sortingOrder = _sortingOrder;
                unit.State = UnitStates.Idle;
                unit.AcquireTile(_tile);

                return unit;
            }
        }
    }
}