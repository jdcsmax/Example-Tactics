using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zinnor.Tactics.Tiles;
using Zinnor.Tactics.Units;

namespace Zinnor.Tactics.Navigation
{
    public static class PathFinder
    {
        /// <summary>
        /// 返回 unit 从 start 移动到 end 所经过的路径
        /// </summary>
        public static List<TileOverlay> AStarSearch(
            Unit unit, TileOverlay start, TileOverlay end,
            IReadOnlyDictionary<Vector2Int, TileOverlay> searchableTiles,
            bool excludeStart = true)
        {
            var selectedTiles = new List<TileOverlay>();
            var priorityTiles = new List<TileOverlay>();
            var neighborTiles = new List<TileOverlay>();

            priorityTiles.Add(start);

            while (priorityTiles.Count > 0)
            {
                var current = priorityTiles.OrderBy(x => x.F).First();

                priorityTiles.Remove(current);
                selectedTiles.Add(current);

                if (current == end)
                {
                    selectedTiles.Clear();
                    selectedTiles.Add(end);
                    return BuildShortestPath(selectedTiles, end, excludeStart ? start : null);
                }

                NavUtils.GetNeighbors(unit, current, searchableTiles, neighborTiles);

                foreach (var neighbor in neighborTiles)
                {
                    if (selectedTiles.Contains(neighbor))
                    {
                        continue;
                    }

                    neighbor.G = NavUtils.GetManhattenDistance(start, neighbor);
                    neighbor.H = NavUtils.GetManhattenDistance(end, neighbor);
                    neighbor.Previous = current;

                    if (priorityTiles.Contains(neighbor))
                    {
                        continue;
                    }

                    priorityTiles.Add(neighbor);
                }
            }

            return new List<TileOverlay>();
        }

        /// <summary>
        /// 返回 unit 从 start 移动到 end 需要的移动消耗
        /// </summary>
        /// <href>https://www.codeproject.com/Articles/1221034/Pathfinding-Algorithms-in-Csharp</href>
        public static List<TileOverlay> DijkstraSearch(
            Unit unit, TileOverlay start, TileOverlay end,
            IReadOnlyDictionary<Vector2Int, TileOverlay> searchableTiles)
        {
            foreach (var tile in searchableTiles.Values)
            {
                tile.W = unit.MoveCost(tile);
                tile.C = int.MaxValue;
                tile.Previous = null;
                tile.Visited = false;
            }

            start.C = 0;
            start.Visited = true;

            var neighborTiles = new List<TileOverlay>();
            var priorityTiles = new List<TileOverlay>() { start };

            do
            {
                var current = priorityTiles.OrderBy(x => x.W).First();

                priorityTiles.Remove(current);

                NavUtils.GetNeighbors(current, searchableTiles, neighborTiles);

                foreach (var neighbor in neighborTiles.Where(n => !n.Visited))
                {
                    if (neighbor.C != int.MaxValue && current.C + neighbor.W >= neighbor.C)
                    {
                        continue;
                    }

                    neighbor.C = current.C + neighbor.W;
                    neighbor.Previous = current;

                    if (priorityTiles.Contains(neighbor))
                    {
                        continue;
                    }

                    priorityTiles.Add(neighbor);
                }

                current.Visited = true;

                if (current == end)
                {
                    break;
                }
            } while (priorityTiles.Any());

            return BuildShortestPath(end);
        }

        private static List<TileOverlay> BuildShortestPath(TileOverlay end)
        {
            return BuildShortestPath(new List<TileOverlay>() { end }, end);
        }

        private static List<TileOverlay> BuildShortestPath(
            List<TileOverlay> shortestPath, TileOverlay end, TileOverlay exclude = null)
        {
            var curr = end;

            while (curr.Previous)
            {
                shortestPath.Add(curr.Previous);

                curr = curr.Previous;
            }

            shortestPath.Reverse();

            if (exclude != null)
            {
                shortestPath.Remove(exclude);
            }

            return shortestPath;
        }
    }
}