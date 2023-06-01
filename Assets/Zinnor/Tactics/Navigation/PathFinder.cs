using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zinnor.Tactics.Navigation;
using Zinnor.Tactics.Tiles;
using Zinnor.Tactics.Units;

namespace Assets.Zinnor.Tactics.Navigation
{
    public static class PathFinder
    {
        /**
         * 返回 unit 从 start 移动到 end 所经过的路径
         */
        public static List<TileOverlay> AStarSearch(
            Unit unit, TileOverlay start, TileOverlay end,
            Dictionary<Vector2Int, TileOverlay> searchableTiles,
            bool excludeStart = true)
        {
            var closedTiles = new List<TileOverlay>();
            var priorityTiles = new List<TileOverlay>();
            var neighborTiles = new List<TileOverlay>();

            priorityTiles.Add(start);

            while (priorityTiles.Count > 0)
            {
                TileOverlay current = priorityTiles.OrderBy(x => x.F).First();

                priorityTiles.Remove(current);
                closedTiles.Add(current);

                if (current == end)
                {
                    closedTiles.Clear();
                    closedTiles.Add(end);
                    BuildShortestPath(closedTiles, end);
                    closedTiles.Reverse();

                    if (excludeStart)
                    {
                        closedTiles.Remove(start);
                    }

                    return closedTiles;
                }

                NavUtils.GetNeighbors(unit, current, searchableTiles, neighborTiles);

                foreach (var neighbor in neighborTiles)
                {
                    if (closedTiles.Contains(neighbor))
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

        /**
         * 返回 unit 从 start 移动到 end 需要的移动消耗
         *
         * Dijkstra Algorithm
         *
         * Reference: https://www.codeproject.com/Articles/1221034/Pathfinding-Algorithms-in-Csharp
         */
        public static List<TileOverlay> DijkstraSearch(
            Unit unit, TileOverlay start, TileOverlay end,
            Dictionary<Vector2Int, TileOverlay> searchableTiles)
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

                foreach (var neighbor in neighborTiles)
                {
                    if (neighbor.Visited)
                    {
                        continue;
                    }
                    
                    if (neighbor.C == int.MaxValue || current.C + neighbor.W < neighbor.C)
                    {
                        neighbor.C = current.C + neighbor.W;
                        neighbor.Previous = current;

                        if (priorityTiles.Contains(neighbor))
                        {
                            continue;
                        }

                        priorityTiles.Add(neighbor);
                    }
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
            var shortestPath = new List<TileOverlay>() { end };
            BuildShortestPath(shortestPath, end);
            shortestPath.Reverse();
            return shortestPath;
        }

        private static void BuildShortestPath(List<TileOverlay> tiles, TileOverlay tile)
        {
            if (tile.Previous != null)
            {
                tiles.Add(tile.Previous);
                BuildShortestPath(tiles, tile.Previous);
            }
        }
    }
}