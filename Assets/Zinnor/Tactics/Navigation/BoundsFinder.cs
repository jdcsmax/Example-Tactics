using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zinnor.Tactics.Tiles;

namespace Zinnor.Tactics.Navigation
{
    public static class BoundsFinder
    {
        public static List<TileOverlay> Find(IEnumerable<TileOverlay> tiles,
            IReadOnlyDictionary<Vector2Int, TileOverlay> movableTiles,
            IReadOnlyDictionary<Vector2Int, TileOverlay> searchableTiles)

        {
            return tiles.Where(tile => IsOutside(tile, movableTiles, searchableTiles))
                .Select(tile => tile)
                .ToList();
        }

        private static bool IsOutside(TileOverlay tile,
            IReadOnlyDictionary<Vector2Int, TileOverlay> movableTiles,
            IReadOnlyDictionary<Vector2Int, TileOverlay> searchableTiles)
        {
            return IsOutside(NavUtils.Top(tile.Location3D), movableTiles, searchableTiles) ||
                   IsOutside(NavUtils.Bottom(tile.Location3D), movableTiles, searchableTiles) ||
                   IsOutside(NavUtils.Right(tile.Location3D), movableTiles, searchableTiles) ||
                   IsOutside(NavUtils.Left(tile.Location3D), movableTiles, searchableTiles);
        }

        private static bool IsOutside(Vector2Int position,
            IReadOnlyDictionary<Vector2Int, TileOverlay> movableTiles,
            IReadOnlyDictionary<Vector2Int, TileOverlay> searchableTiles)
        {
            return !movableTiles.ContainsKey(position) && searchableTiles.ContainsKey(position);
        }
    }
}