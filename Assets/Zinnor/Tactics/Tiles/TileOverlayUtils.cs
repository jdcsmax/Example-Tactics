using System.Collections.Generic;
using Zinnor.Tactics.Units;

namespace Zinnor.Tactics.Tiles
{
    public static class TileOverlayUtils
    {
        public static void ShowMoveSquares(Unit unit, TileOverlay start, List<TileOverlay> movableTiles)
        {
            foreach (var tile in movableTiles)
            {
                if (tile == start)
                {
                    continue;
                }

                unit.ShowMoveSprite(tile);
            }
        }

        public static void ShowMoveArrows(Unit unit, TileOverlay start, List<TileOverlay> pathTiles)
        {
            for (int i = 0; i < pathTiles.Count; i++)
            {
                TileOverlay previousTile = i > 0 ? pathTiles[i - 1] : start;
                TileOverlay currentTile = pathTiles[i];
                TileOverlay futureTile = i + 1 < pathTiles.Count ? pathTiles[i + 1] : null;
                currentTile.ShowArrowSprite(TileArrowTranslator.Direction(previousTile, currentTile, futureTile));
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