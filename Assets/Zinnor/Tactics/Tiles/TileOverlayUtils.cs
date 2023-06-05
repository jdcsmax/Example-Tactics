using System.Collections.Generic;
using System.Linq;
using Zinnor.Tactics.Units;

namespace Zinnor.Tactics.Tiles
{
    public static class TileOverlayUtils
    {
        public static void ShowMoveArrows(Unit unit, TileOverlay start, List<TileOverlay> pathTiles)
        {
            for (var i = 0; i < pathTiles.Count; i++)
            {
                var previousTile = i > 0 ? pathTiles[i - 1] : start;
                var currentTile = pathTiles[i];
                var futureTile = i + 1 < pathTiles.Count ? pathTiles[i + 1] : null;
                unit.ShowMoveArrow(currentTile, TileArrowTranslator.Direction(previousTile, currentTile, futureTile));
            }
        }

        public static void ShowMoveSquares(Unit unit, TileOverlay start, List<TileOverlay> movableTiles)
        {
            foreach (var tile in movableTiles.Where(tile => tile != start))
            {
                unit.ShowMoveSprite(tile);
            }
        }

        public static void ShowAttackSquares(Unit unit, List<TileOverlay> attackableTiles)
        {
            foreach (var tile in attackableTiles)
            {
                unit.ShowAttackSprite(tile);
            }
        }
    }
}