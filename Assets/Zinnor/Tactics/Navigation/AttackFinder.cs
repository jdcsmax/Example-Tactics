using System.Collections.Generic;
using UnityEngine;
using Zinnor.Tactics.Tiles;
using Zinnor.Tactics.Units;

namespace Zinnor.Tactics.Navigation
{
    public static class AttackFinder
    {
        /**
         * 返回可以攻击的 Tile 列表
         */
        public static Dictionary<Vector2Int, TileOverlay> Find(
            Unit unit, TileOverlay start,
            Dictionary<Vector2Int, TileOverlay> movableTiles,
            Dictionary<Vector2Int, TileOverlay> searchableTiles)
        {
            var attackableTiles = new Dictionary<Vector2Int, TileOverlay>();
            IEnumerable<TileOverlay> tiles = movableTiles.Count > 0
                ? movableTiles.Values
                : new List<TileOverlay> { start };

            foreach (var tile in tiles)
            {
                for (var x = -unit.MaxAttackDistance; x <= unit.MaxAttackDistance; ++x)
                {
                    for (var y = -unit.MaxAttackDistance; y <= unit.MaxAttackDistance; ++y)
                    {
                        var location = new Vector2Int(x + tile.X, y + tile.Y);

                        if (Attackable(location, tile, unit, movableTiles, attackableTiles))
                        {
                            if (searchableTiles.TryGetValue(location, out var attackable))
                            {
                                if (attackable.Traverable)
                                {
                                    attackableTiles.Add(location, attackable);
                                }
                            }
                        }
                    }
                }
            }

            return attackableTiles;
        }

        private static bool Attackable(
            Vector2Int location, TileOverlay tile, Unit unit,
            Dictionary<Vector2Int, TileOverlay> movableTiles,
            Dictionary<Vector2Int, TileOverlay> attackableTiles)
        {
            var n = NavUtils.GetManhattenDistance(location, tile.Location2D);

            if (n < unit.MinAttackDistance || n > unit.MaxAttackDistance)
            {
                return false;
            }

            if (movableTiles.ContainsKey(location))
            {
                return false;
            }

            if (attackableTiles.ContainsKey(location))
            {
                return false;
            }

            return true;
        }
    }
}