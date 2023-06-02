using UnityEngine;

namespace Zinnor.Tactics.Tiles
{
    public static class TileArrowTranslator
    {
        public static TileArrowDirection Direction(TileOverlay previousTile, TileOverlay currentTile,
            TileOverlay futureTile)
        {
            bool isFinal = futureTile == null;

            Vector2Int pastDirection = previousTile != null
                ? currentTile.Location2D - previousTile.Location2D
                : new Vector2Int(0, 0);
            Vector2Int futureDirection = futureTile != null
                ? futureTile.Location2D - currentTile.Location2D
                : new Vector2Int(0, 0);
            Vector2Int direction = pastDirection != futureDirection
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
                if (pastDirection.y < futureDirection.y)
                {
                    return TileArrowDirection.DownLeft;
                }

                return TileArrowDirection.UpRight;
            }

            if (direction == new Vector2Int(-1, 1))
            {
                if (pastDirection.y < futureDirection.y)
                {
                    return TileArrowDirection.DownRight;
                }

                return TileArrowDirection.UpLeft;
            }

            if (direction == new Vector2Int(1, -1))
            {
                if (pastDirection.y > futureDirection.y)
                {
                    return TileArrowDirection.UpLeft;
                }

                return TileArrowDirection.DownRight;
            }

            if (direction == new Vector2Int(-1, -1))
            {
                if (pastDirection.y > futureDirection.y)
                {
                    return TileArrowDirection.UpRight;
                }

                return TileArrowDirection.DownLeft;
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