using System;
using System.Collections.Generic;
using UnityEngine;
using Zinnor.Tactics.Tiles;

namespace Zinnor.Tactics.Navigation
{
    public static class BoundsFinder
    {
        public static List<TileOverlay> Find(IEnumerable<TileOverlay> movableTiles,
            Func<Vector2Int, bool> movable, Func<Vector2Int, bool> searchable)
        {
            var boundsTiles = new List<TileOverlay>();

            foreach (var item in movableTiles)
            {
                if (IsOutside(item, movable, searchable))
                {
                    boundsTiles.Add(item);
                }
            }

            return boundsTiles;
        }

        public static bool IsOutside(TileOverlay tile,
            Func<Vector2Int, bool> movable, Func<Vector2Int, bool> searchable)
        {
            return IsOutside(NavUtils.Top(tile.Grid3DLocation), movable, searchable) ||
                   IsOutside(NavUtils.Bottom(tile.Grid3DLocation), movable, searchable) ||
                   IsOutside(NavUtils.Right(tile.Grid3DLocation), movable, searchable) ||
                   IsOutside(NavUtils.Left(tile.Grid3DLocation), movable, searchable);
        }

        private static bool IsOutside(Vector2Int position,
            Func<Vector2Int, bool> movable, Func<Vector2Int, bool> searchable)
        {
            if (movable.Invoke(position))
            {
                return false;
            }

            return searchable.Invoke(position);
        }
    }
}