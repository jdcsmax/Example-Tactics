using System.Collections.Generic;
using UnityEngine;
using Zinnor.Supports;
using Zinnor.Tactics.Units;

namespace Zinnor.Tactics.Tiles
{
    public class TileOverlay : MonoBehaviour
    {
        /**
         * 缓存 Dijkstra 算法中 g 函数计算结果
         */
        public int W;

        /**
         * 缓存 Dijkstra 算法中 f 函数计算结果
         */
        public int C;

        /**
         * 标记 Dijkstra 算法中是否以访问
         */
        public bool Visited;

        /**
         * 缓存 A* 算法中 g 函数计算结果
         */
        public int G;

        /**
         * 缓存 A* 算法中 h 函数计算结果
         */
        public int H;

        /**
         * 返回 A* 算法中 f 函数计算结果
         */
        public int F => G + H;

        public ScriptableTile Data;
        public TileOverlay Previous;

        public Vector3Int Grid3DLocation;

        public int GridX => Grid3DLocation.x;
        public int GridY => Grid3DLocation.y;
        public int GridZ => Grid3DLocation.z;

        public Vector2Int Grid2DLocation => new Vector2Int(Grid3DLocation.x, Grid3DLocation.y);

        [SerializeField] private SpriteRenderer _tileSpriteRenderer;
        [SerializeField] private SpriteRenderer _arrowSpriteRenderer;

        /**
         * 指针精灵
         */
        public List<Sprite> ArrowSprites;

        /**
         * 治疗精灵
         */
        public Sprite HealSprite;

        /**
         * 攻击精灵
         */
        public Sprite AttackSprite;

        /**
         * 移动精灵
         */
        public Sprite MoveSprite;

        public SpriteRenderer TileSpriteRenderer
        {
            get
            {
                if (_tileSpriteRenderer == null)
                {
                    _tileSpriteRenderer = GetComponent<SpriteRenderer>();
                }

                return _tileSpriteRenderer;
            }
        }

        public SpriteRenderer ArrowSpriteRenderer
        {
            get
            {
                if (_arrowSpriteRenderer == null)
                {
                    _arrowSpriteRenderer = GetComponentInChildren<SpriteRenderer>();
                }

                return _arrowSpriteRenderer;
            }
        }

        // 占据 Tile 的 Unit
        public Unit Unit;

        /**
         * 显示治疗覆盖精灵
         */
        public void ShowHealSprite()
        {
            ShowOverlaySprite(HealSprite);
        }

        /**
         * 显示攻击覆盖精灵
         */
        public void ShowAttackSprite()
        {
            ShowOverlaySprite(AttackSprite);
        }

        /**
         * 显示移动覆盖精灵
         */
        public void ShowMoveSprite()
        {
            ShowOverlaySprite(MoveSprite);
        }

        private void ShowOverlaySprite(Sprite sprite)
        {
            TileSpriteRenderer.sprite = sprite;
            TileSpriteRenderer.color = Colors.Square;
        }

        public void HideOverlaySprite()
        {
            TileSpriteRenderer.color = Colors.Transparent;

            ShowArrowSprite(TileArrowDirection.None);
        }

        public void ShowArrowSprite(TileArrowDirection direction)
        {
            ArrowSpriteRenderer.sortingOrder = 1;

            if (direction == TileArrowDirection.None)
            {
                ArrowSpriteRenderer.color = Colors.Transparent;
            }
            else
            {
                ArrowSpriteRenderer.color = Colors.Opaque;
                ArrowSpriteRenderer.sprite = ArrowSprites[(int)direction];
            }
        }
    }
}