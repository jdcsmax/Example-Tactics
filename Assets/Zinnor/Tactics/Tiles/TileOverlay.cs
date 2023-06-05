using UnityEngine;
using Zinnor.Tactics.Units;

namespace Zinnor.Tactics.Tiles
{
    public class TileOverlay : MonoBehaviour
    {
        /// <summary>
        /// 缓存 Dijkstra 算法中 g 函数计算结果
        /// </summary>
        public int W;

        /// <summary>
        /// 缓存 Dijkstra 算法中 f 函数计算结果
        /// </summary>
        public int C;

        /// <summary>
        /// 标记 Dijkstra 算法中是否以访问
        /// </summary>
        public bool Visited;

        /// <summary>
        /// 缓存 A* 算法中 g 函数计算结果
        /// </summary>
        public int G;

        /// <summary>
        /// 缓存 A* 算法中 h 函数计算结果
        /// </summary>
        public int H;

        /// <summary>
        /// 返回 A* 算法中 f 函数计算结果
        /// </summary>
        public int F => G + H;

        /// <summary>
        /// 对路径搜索中前一个格子的引用
        /// </summary>
        public TileOverlay Previous;

        /// <summary>
        /// 缓存格子在 Tilemap 中三维坐标
        /// </summary>
        public Vector3Int Location3D;

        public int X => Location3D.x;
        public int Y => Location3D.y;
        public int Z => Location3D.z;

        /// <summary>
        /// 返回格子在 Tilemap 中二维坐标
        /// </summary>
        public Vector2Int Location2D => new Vector2Int(Location3D.x, Location3D.y);

        /// <summary>
        /// 格子精灵绘制器
        /// </summary>
        public SpriteRenderer TileSpriteRenderer;

        /// <summary>
        /// 箭头精灵绘制器
        /// </summary>
        public SpriteRenderer ArrowSpriteRenderer;

        /// <summary>
        /// 数据配置
        /// </summary>
        public TileOverlayData OverlayData;

        /// <summary>
        /// 可否步行通过
        /// </summary>
        public bool Walkable => OverlayData.Walkable;

        /// <summary>
        /// 可否飞行通过
        /// </summary>
        public bool Flyable => OverlayData.Flyable;

        /// <summary>
        /// 可否通过
        /// </summary>
        public bool Traverable => OverlayData.Traverable;

        /// <summary>
        /// 占有单位
        /// </summary>
        public Unit Occupier;

        public static Builder NewBuilder()
        {
            return new Builder();
        }

        public sealed class Builder
        {
            private TileOverlay _tilePrefab;
            private GameObject _tileContainer;
            private Vector3Int _tileLocation;
            private Vector3 _worldPosition;
            private int _sortingOrder;

            public Builder SetTilePrefab(TileOverlay tileOverlay)
            {
                _tilePrefab = tileOverlay;
                return this;
            }

            public Builder SetTileContainer(GameObject tileContainer)
            {
                _tileContainer = tileContainer;
                return this;
            }

            public Builder SetTileLocation(Vector3Int tileLocation)
            {
                _tileLocation = tileLocation;
                return this;
            }

            public Builder SetWorldPosition(Vector3 worldPosition)
            {
                _worldPosition = worldPosition;
                return this;
            }

            public Builder SetSortingOrder(int sortingOrder)
            {
                _sortingOrder = sortingOrder;
                return this;
            }

            public TileOverlay Build()
            {
                var tileOverlay = Instantiate(_tilePrefab, _tileContainer.transform);
                tileOverlay.Location3D = _tileLocation;
                tileOverlay.transform.position = new Vector3(
                    _worldPosition.x, _worldPosition.y, _worldPosition.z + 1);
                tileOverlay.TileSpriteRenderer.sortingOrder = _sortingOrder;
                tileOverlay.ArrowSpriteRenderer.sortingOrder = _sortingOrder + 1;
                return tileOverlay;
            }
        }
    }
}