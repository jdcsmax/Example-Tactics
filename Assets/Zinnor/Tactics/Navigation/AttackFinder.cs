using System.Collections.Generic;
using UnityEngine;
using Zinnor.Tactics.Tiles;
using Zinnor.Tactics.Units;

namespace Zinnor.Tactics.Navigation
{
    public static class AttackFinder
    {
        /// <summary>
        /// 返回可以攻击的 Tile 列表
        /// </summary>
        public static Dictionary<Vector2Int, TileOverlay> Find(
            Unit unit, TileOverlay start,
            IReadOnlyDictionary<Vector2Int, TileOverlay> movableTiles,
            IReadOnlyDictionary<Vector2Int, TileOverlay> searchableTiles)
        {
            var attackableTiles = new Dictionary<Vector2Int, TileOverlay>();
            var tiles = movableTiles.Count > 0
                ? movableTiles.Values
                : new List<TileOverlay> { start };

            foreach (var tile in tiles)
            {
                for (var x = -unit.MaxAttackRange; x <= unit.MaxAttackRange; ++x)
                {
                    for (var y = -unit.MaxAttackRange; y <= unit.MaxAttackRange; ++y)
                    {
                        var location = new Vector2Int(x + tile.X, y + tile.Y);

                        if (!searchableTiles.TryGetValue(location, out var attackable))
                        {
                            continue;
                        }

                        if (!attackable.Traverable)
                        {
                            continue;
                        }

                        if (!Attackable(location, tile, unit, movableTiles, attackableTiles))
                        {
                            continue;
                        }

                        attackableTiles.Add(location, attackable);
                    }
                }
            }

            return attackableTiles;
        }

        private static bool Attackable(
            Vector2Int location, TileOverlay tile, Unit unit,
            IReadOnlyDictionary<Vector2Int, TileOverlay> movableTiles,
            IReadOnlyDictionary<Vector2Int, TileOverlay> attackableTiles)
        {
            var n = NavUtils.GetManhattenDistance(location, tile.Location2D);

            if (n < unit.MinAttackRange || n > unit.MaxAttackRange)
            {
                return false;
            }

            return !movableTiles.ContainsKey(location) && !attackableTiles.ContainsKey(location);
        }
    }
}