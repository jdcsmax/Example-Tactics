using UnityEngine;

namespace Zinnor.Tactics.Tiles
{
    public static class TileArrowTranslator
    {
        public static TileArrowDirection Direction(
            TileOverlay previousTile, TileOverlay currentTile, TileOverlay futureTile)
        {
            var isFinal = futureTile == null;
            var pastDirection = previousTile != null
                ? currentTile.Location2D - previousTile.Location2D
                : new Vector2Int(0, 0);
            var futureDirection = futureTile != null
                ? futureTile.Location2D - currentTile.Location2D
                : new Vector2Int(0, 0);
            var direction = pastDirection != futureDirection
                ? pastDirection + futureDirection
                : futureDirection;

            if (direction == new Vector2Int(0, 1) && !isFinal)
            {
                return TileArrowDirection.Up;
            }

            if (direction == new Vector2Int(0, -1) && !isFinal)
            {
                return TileArrowDirection.Down;
            }

            if (direction == new Vector2Int(1, 0) && !isFinal)
            {
                return TileArrowDirection.Right;
            }

            if (direction == new Vector2Int(-1, 0) && !isFinal)
            {
                return TileArrowDirection.Left;
            }

            if (direction == new Vector2Int(1, 1))
            {
                return pastDirection.y < futureDirection.y
                    ? TileArrowDirection.DownLeft
                    : TileArrowDirection.UpRight;
            }

            if (direction == new Vector2Int(-1, 1))
            {
                return pastDirection.y < futureDirection.y
                    ? TileArrowDirection.DownRight
                    : TileArrowDirection.UpLeft;
            }

            if (direction == new Vector2Int(1, -1))
            {
                return pastDirection.y > futureDirection.y
                    ? TileArrowDirection.UpLeft
                    : TileArrowDirection.DownRight;
            }

            if (direction == new Vector2Int(-1, -1))
            {
                return pastDirection.y > futureDirection.y
                    ? TileArrowDirection.UpRight
                    : TileArrowDirection.DownLeft;
            }

            if (direction == new Vector2Int(0, 1) && isFinal)
            {
                return TileArrowDirection.EndUp;
            }

            if (direction == new Vector2Int(0, -1) && isFinal)
            {
                return TileArrowDirection.EndDown;
            }

            if (direction == new Vector2Int(1, 0) && isFinal)
            {
                return TileArrowDirection.EndRight;
            }

            if (direction == new Vector2Int(-1, 0) && isFinal)
            {
                return TileArrowDirection.EndLeft;
            }

            return TileArrowDirection.None;
        }
    }
}