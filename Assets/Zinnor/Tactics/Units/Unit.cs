using UnityEngine;
using Zinnor.Tactics.Scriptables.Abilities;
using Zinnor.Tactics.Scriptables.Classes;
using Zinnor.Tactics.Scriptables.Weapons;
using Zinnor.Tactics.Tiles;

namespace Zinnor.Tactics.Units
{
    public class Unit : MonoBehaviour
    {
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
        /// 位置
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
        /// 最小攻击距离
        /// </summary>
        public int MinAttackDistance => Weapon.MinRange;

        /// <summary>
        /// 最大攻击距离
        /// </summary>
        public int MaxAttackDistance => Weapon.MaxRange;

        /**
         * 单位状态
         */
        public UnitStates State;

        /**
         * 单位动画器
         */
        public UnitAnimator Animator;

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

        /**
         * 能否通过
         */
        public bool Traverable(TileOverlay tile)
        {
            if (tile.Walkable)
            {
                return Traverable(tile.Holder);
            }

            if (tile.Flyable && Flying)
            {
                return Traverable(tile.Holder);
            }

            return false;
        }

        /**
         * 能否通过
         */
        public bool Traverable(Unit unit)
        {
            return unit == null || Faction == unit.Faction;
        }

        /**
         * 移动消耗
         */
        public int MoveCost(TileOverlay tile)
        {
            // todo: 根据地形和职业计算移动消耗
            return 1;
        }

        public void ShowMoveSprite(TileOverlay tile)
        {
            // todo: 通过职业配置数据来确定移动到 tile 需要的移动消耗
            tile.ShowMoveSprite();
        }

        public void ShowAttackSprite(TileOverlay tile)
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

                unit.Tile = _tile;
                unit.transform.position = _tile.transform.position;
                _tile.Holder = unit;

                unit.GetComponent<SpriteRenderer>().sortingOrder = _sortingOrder;

                unit.State = UnitStates.Idle;

                unit.AfterPropertySet();

                return unit;
            }
        }
    }
}