using System.Collections.Generic;
using UnityEngine;
using Zinnor.Tactics.Tiles;
using Zinnor.Tactics.Units;

namespace Zinnor.Tactics.Navigation
{
    public static class NavUtils
    {
        /**
         * 获取 current 四周有效的邻近 Tile
         */
        public static void GetNeighbors(Unit unit, TileOverlay current,
            Dictionary<Vector2Int, TileOverlay> searchableTiles, List<TileOverlay> neighborTiles)
        {
            neighborTiles.Clear();

            if (current == null)
            {
                return;
            }

            // Top
            GetNeighbor(Top(current.Location3D), unit, current, searchableTiles, neighborTiles);

            // Bottom
            GetNeighbor(Bottom(current.Location3D), unit, current, searchableTiles, neighborTiles);

            // Right
            GetNeighbor(Right(current.Location3D), unit, current, searchableTiles, neighborTiles);

            // Left
            GetNeighbor(Left(current.Location3D), unit, current, searchableTiles, neighborTiles);
        }

        public static void GetNeighbor(Vector2Int location, Unit unit, TileOverlay current,
            Dictionary<Vector2Int, TileOverlay> searchableTiles, List<TileOverlay> neighborTiles)
        {
            if (searchableTiles.TryGetValue(location, out TileOverlay tile))
            {
                if (unit.Traverable(tile))
                {
                    if (Mathf.Abs(current.Z - tile.Z) <= 1)
                    {
                        neighborTiles.Add(tile);
                    }
                }
            }
        }

        public static void GetNeighbors(TileOverlay current,
            Dictionary<Vector2Int, TileOverlay> searchableTiles,
            List<TileOverlay> neighborTiles)
        {
            neighborTiles.Clear();

            if (current == null)
            {
                return;
            }

            // Top
            GetNeighbor(Top(current.Location3D), searchableTiles, neighborTiles);

            // Bottom
            GetNeighbor(Bottom(current.Location3D), searchableTiles, neighborTiles);

            // Right
            GetNeighbor(Right(current.Location3D), searchableTiles, neighborTiles);

            // Left
            GetNeighbor(Left(current.Location3D), searchableTiles, neighborTiles);
        }

        public static void GetNeighbor(Vector2Int location,
            Dictionary<Vector2Int, TileOverlay> searchableTiles,
            List<TileOverlay> neighborTiles)
        {
            if (searchableTiles.TryGetValue(location, out TileOverlay tile))
            {
                neighborTiles.Add(tile);
            }
        }

        /**
         * 返回 Tile 之间的曼哈顿距离
         */
        public static int GetManhattenDistance(TileOverlay tile, TileOverlay neighbor)
        {
            return Mathf.Abs(tile.Location3D.x - neighbor.Location3D.x) +
                   Mathf.Abs(tile.Location3D.y - neighbor.Location3D.y);
        }

        public static int GetManhattenDistance(Vector3Int position, Vector3Int neighbor)
        {
            return Mathf.Abs(position.x - neighbor.x) +
                   Mathf.Abs(position.y - neighbor.y);
        }

        public static int GetManhattenDistance(Vector2Int position, Vector2Int neighbor)
        {
            return Mathf.Abs(position.x - neighbor.x) +
                   Mathf.Abs(position.y - neighbor.y);
        }

        public static Vector2Int Top(Vector3Int position) => Top(position, 1);

        public static Vector2Int Bottom(Vector3Int position) => Bottom(position, 1);

        public static Vector2Int Right(Vector3Int position) => Right(position, 1);

        public static Vector2Int Left(Vector3Int position) => Left(position, 1);

        public static Vector2Int Top(Vector3Int position, int distance) => new(position.x, position.y + distance);

        public static Vector2Int Bottom(Vector3Int position, int distance) => new(position.x, position.y - distance);

        public static Vector2Int Right(Vector3Int position, int distance) => new(position.x + distance, position.y);

        public static Vector2Int Left(Vector3Int position, int distance) => new(position.x - distance, position.y);
    }
}