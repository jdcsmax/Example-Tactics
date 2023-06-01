using System.Collections.Generic;
using UnityEngine;
using Zinnor.Supports;
using Zinnor.Tactics.Units;

namespace Zinnor.Tactics.Tiles
{
    public class TileOverlay : MonoBehaviour
    {
        /**
         * ���� Dijkstra �㷨�� g ����������
         */
        public int W;

        /**
         * ���� Dijkstra �㷨�� f ����������
         */
        public int C;

        /**
         * ��� Dijkstra �㷨���Ƿ��Է���
         */
        public bool Visited;

        /**
         * ���� A* �㷨�� g ����������
         */
        public int G;

        /**
         * ���� A* �㷨�� h ����������
         */
        public int H;

        /**
         * ���� A* �㷨�� f ����������
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
         * ָ�뾫��
         */
        public List<Sprite> ArrowSprites;

        /**
         * ���ƾ���
         */
        public Sprite HealSprite;

        /**
         * ��������
         */
        public Sprite AttackSprite;

        /**
         * �ƶ�����
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

        // ռ�� Tile �� Unit
        public Unit Unit;

        /**
         * ��ʾ���Ƹ��Ǿ���
         */
        public void ShowHealSprite()
        {
            ShowOverlaySprite(HealSprite);
        }

        /**
         * ��ʾ�������Ǿ���
         */
        public void ShowAttackSprite()
        {
            ShowOverlaySprite(AttackSprite);
        }

        /**
         * ��ʾ�ƶ����Ǿ���
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