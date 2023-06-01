using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zinnor.Tactics.Navigation;
using Zinnor.Tactics.Tiles;
using Zinnor.Tactics.Units;

namespace Assets.Zinnor.Tactics.Navigation
{
    public static class MoveFinder
    {
        /**
         * 返回可以移动的 Tile 列表
         */
        public static Dictionary<Vector2Int, TileOverlay> Find(
            Unit unit, TileOverlay start, Dictionary<Vector2Int, TileOverlay> searchableTiles)
        {
            var tiles = new HashSet<TileOverlay>();
            var neighborTiles = new List<TileOverlay>();
            var rangeTiles = new Dictionary<Vector2Int, TileOverlay>();
            var moveableTiles = new Dictionary<Vector2Int, TileOverlay>();
            int distance = 0;

            tiles.Add(start);

            var previousTiles = new HashSet<TileOverlay>() { start };

            var mov = unit.Mov;

            while (distance < mov)
            {
                var surroundTiles = new HashSet<TileOverlay>();

                foreach (var tile in previousTiles)
                {
                    NavUtils.GetNeighbors(unit, tile, searchableTiles, neighborTiles);

                    surroundTiles.UnionWith(neighborTiles);
                }

                tiles.UnionWith(surroundTiles);
                previousTiles = surroundTiles;
                distance++;
            }

            foreach (var tile in tiles)
            {
                rangeTiles.Add(tile.Grid2DLocation, tile);
            }

            foreach (var tile in rangeTiles)
            {
                var shortestPath = PathFinder.DijkstraSearch(unit, start, tile.Value, rangeTiles);

                var cost = shortestPath.Count > 0 ? shortestPath[^1].C : 0;

                if (unit.Mov >= cost)
                {
                    moveableTiles.Add(tile.Value.Grid2DLocation, tile.Value);
                }
            }

            return moveableTiles;
        }
    }
}