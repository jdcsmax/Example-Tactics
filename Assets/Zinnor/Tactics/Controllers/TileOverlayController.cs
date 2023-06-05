using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zinnor.Supports;
using Zinnor.Tactics.Tiles;
using Zinnor.Tactics.Units;

namespace Zinnor.Tactics.Controllers
{
    public class TileOverlayController : MonoBehaviour
    {
        /// <summary>
        /// 路径箭头
        /// </summary>
        public List<Sprite> ArrowSprites;

        /// <summary>
        /// 辅助格子
        /// </summary>
        public Sprite AssistSprite;

        /// <summary>
        /// 攻击格子
        /// </summary>
        public Sprite AttackSprite;

        /// <summary>
        /// 移动格子
        /// </summary>
        public Sprite MovementSprite;

        public void ShowActionSquares(Unit unit, TileOverlay start, IEnumerable<TileOverlay> tiles)
        {
            if (unit.Weapon == null)
            {
                foreach (var tile in tiles.Where(tile => tile != start))
                {
                    HideTileSprite(tile);
                }
            }
            else if (unit.Weapon.Assist)
            {
                foreach (var tile in tiles.Where(tile => tile != start))
                {
                    ShowTileSprite(tile.TileSpriteRenderer, AssistSprite, Colors.Square);
                }
            }
            else
            {
                foreach (var tile in tiles.Where(tile => tile != start))
                {
                    ShowTileSprite(tile.TileSpriteRenderer, AttackSprite, Colors.Square);
                }
            }
        }

        public void ShowMovementSquares(TileOverlay start, IEnumerable<TileOverlay> movableTiles)
        {
            foreach (var tile in movableTiles.Where(tile => tile != start))
            {
                ShowTileSprite(tile.TileSpriteRenderer, MovementSprite, Colors.Square);
            }
        }

        public void ShowPathingArrows(TileOverlay start, List<TileOverlay> pathingTiles)
        {
            for (var i = 0; i < pathingTiles.Count; i++)
            {
                var previousTile = i > 0 ? pathingTiles[i - 1] : start;
                var currentTile = pathingTiles[i];
                var futureTile = i + 1 < pathingTiles.Count ? pathingTiles[i + 1] : null;
                var direction = TileArrowTranslator.Direction(previousTile, currentTile, futureTile);

                if (direction != TileArrowDirection.None)
                {
                    currentTile.ArrowSpriteRenderer.sprite = ArrowSprites[(int)direction];
                    currentTile.ArrowSpriteRenderer.color = Colors.Opaque;
                }
                else
                {
                    currentTile.ArrowSpriteRenderer.color = Colors.Transparent;
                }
            }
        }

        private static void ShowTileSprite(SpriteRenderer renderer, Sprite sprite, Color color)
        {
            renderer.sprite = sprite;
            renderer.color = color;
        }

        private static void HideTileSprite(TileOverlay tile)
        {
            tile.TileSpriteRenderer.color = Colors.Transparent;
            tile.ArrowSpriteRenderer.color = Colors.Transparent;
        }
    }
}