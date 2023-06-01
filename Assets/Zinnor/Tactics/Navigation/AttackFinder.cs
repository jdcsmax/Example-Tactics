using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zinnor.Tactics.Navigation;
using Zinnor.Tactics.Tiles;
using Zinnor.Tactics.Units;

namespace Assets.Zinnor.Tactics.Navigation
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
                        var pos = new Vector2Int(x + tile.GridX, y + tile.GridY);

                        var n = NavUtils.GetManhattenDistance(pos, tile.Grid2DLocation);

                        if (n < unit.MinAttackDistance || n > unit.MaxAttackDistance)
                        {
                            continue;
                        }

                        if (attackableTiles.ContainsKey(pos))
                        {
                            continue;
                        }

                        if (movableTiles.ContainsKey(pos))
                        {
                            continue;
                        }

                        if (searchableTiles.TryGetValue(pos, out var attackable))
                        {
                            attackableTiles.Add(pos, attackable);
                        }
                    }
                }
            }

            return attackableTiles;
        }
    }
}