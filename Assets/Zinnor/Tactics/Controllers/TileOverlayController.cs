using System.Collections.Generic;
using UnityEngine;
using Zinnor.Tactics.Tiles;

namespace Zinnors.Tactics.Controllers
{
    public class TileOverlayController : MonoBehaviour
    {
        private static TileOverlayController _instance;

        public static TileOverlayController Instance => _instance;

        public Color AttackRangeColor;
        public Color MoveRangeColor;
        public Color BlockedTileColor;

        public Dictionary<Color, List<TileOverlay>> ColoredTiles;

        private void Awake()
        {
            if (_instance != null && _instance != this)
            {
                Destroy(this.gameObject);
            }
            else
            {
                _instance = this;
            }

            ColoredTiles = new Dictionary<Color, List<TileOverlay>>();
        }

        // public void ClearTiles(Color? color = null)
        // {
        //     if (color.HasValue)
        //     {
        //         if (ColoredTiles.ContainsKey(color.Value))
        //         {
        //             var tiles = ColoredTiles[color.Value];
        //             ColoredTiles.Remove(color.Value);
        //
        //             foreach (var coloredTile in tiles)
        //             {
        //                 coloredTile.HideTile();
        //
        //                 foreach (var usedColors in ColoredTiles.Keys)
        //                 {
        //                     foreach (var usedTile in ColoredTiles[usedColors])
        //                     {
        //                         if (coloredTile.Grid2DLocation == usedTile.Grid2DLocation)
        //                         {
        //                             coloredTile.ShowTile(usedColors);
        //                         }
        //                     }
        //                 }
        //             }
        //         }
        //     }
        //     else
        //     {
        //         foreach (var item in ColoredTiles.Keys)
        //         {
        //             foreach (var tile in ColoredTiles[item])
        //             {
        //                 tile.HideTile();
        //             }
        //         }
        //
        //         ColoredTiles.Clear();
        //     }
        // }
        //
        // public void ShowTiles(Sprite sprite, List<TileOverlay> overlayTiles)
        // {
        //     ClearTiles(color);
        //
        //     foreach (var tile in overlayTiles)
        //     {
        //         tile.ShowTile(sprite);
        //
        //         if (tile.Blocked)
        //         {
        //             tile.HideTile();
        //         }
        //     }
        //
        //     ColoredTiles.Add(color, overlayTiles);
        // }
        //
        // public void ColorSingleTile(Color color, TileOverlay tile)
        // {
        //     ClearTiles(color);
        //     tile.ShowTile(color);
        //
        //     if (tile.Blocked)
        //     {
        //         tile.ShowTile(BlockedTileColor);
        //     }
        //
        //     ColoredTiles.Add(color, new List<TileOverlay> { tile });
        // }
    }
}