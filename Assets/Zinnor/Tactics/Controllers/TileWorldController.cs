using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using log4net;
using UnityEngine;
using UnityEngine.Tilemaps;
using Zinnor.Tactics.Navigation;
using Zinnor.Tactics.Scriptables.Abilities;
using Zinnor.Tactics.Scriptables.Weapons;
using Zinnor.Tactics.Tiles;
using Zinnor.Tactics.Units;
using Random = UnityEngine.Random;

namespace Zinnor.Tactics.Controllers
{
    public class TileWorldController : MonoBehaviour
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(TileWorldController));

        public TileOverlay TileOverlayPrefab;
        public GameObject TileOverlayContainer;
        public TileOverlayDataSet tileOverlayDataSet;
        public Tilemap TileMask;
        public Tilemap TileGround;
        public TileBounds TileBounds;

        private readonly Dictionary<Vector2Int, TileOverlay> TileOverlayMap = new();
        private readonly Dictionary<TileBase, TileOverlayData> TileOverlayDataMap = new();

        public Unit ActiveUnit;

        #region Test
        public TileOverlayController TileOverlayController;
        public int UnitId;
        public GameObject UnitContainer;
        public Unit UnitPrefab;
        public ScriptableWeapon UnitWeapon;
        #endregion

        private void Awake()
        {
            BuildMap();
        }

        private void Start()
        {
            TestMovementSquares().Forget();
        }
        
        public TileOverlay GetOverlayTileByWorldPosition(Vector3 worldPosition)
        {
            var cellPos = TileGround.WorldToCell(worldPosition);
            var tilePos = new Vector2Int(cellPos.x, cellPos.y);
            return TileOverlayMap.TryGetValue(tilePos, out var tile) ? tile : null;
        }
        
        private async UniTask TestMovementSquares()
        {
            var location = new Vector2Int(-10, -8);
            var extra = new Stats();
            var tile = TileOverlayMap[location];
            ActiveUnit = Unit.newBuilder()
                .SetId(UnitId)
                .SetPrefab(UnitPrefab)
                .SetParent(UnitContainer)
                .SetWeapon(UnitWeapon)
                .SetExtra(extra)
                .SetTile(tile)
                .SetSortingOrder(1)
                .Build();
            ActiveUnit.AfterPropertySet();

            var movableTiles = MoveFinder.Find(ActiveUnit, tile, TileOverlayMap);
            TileOverlayController.ShowMovementSquares(tile, movableTiles.Values);

            var attackableTiles = AttackFinder.Find(ActiveUnit, tile, movableTiles, TileOverlayMap);
            TileOverlayController.ShowActionSquares(ActiveUnit, tile, attackableTiles.Values);

            var boundsTiles = BoundsFinder.Find(movableTiles.Values, movableTiles, TileOverlayMap);
            
            if (boundsTiles.Count != 0)
            {
                var end = boundsTiles[Random.Range(0, boundsTiles.Count)];
                var pathingTiles = PathFinder.AStarSearch(ActiveUnit, tile, end, movableTiles);
                TileOverlayController.ShowPathingArrows(tile, pathingTiles);
            }

            foreach (var i in Enumerable.Range(0, 2))
            {
                ActiveUnit.Faction = i;
                ActiveUnit.AfterControllerChanged();
                
                foreach (UnitStates state in Enum.GetValues(typeof(UnitStates)))
                {
                    ActiveUnit.State = state;
                    ActiveUnit.AfterStateChanged();
                    await UniTask.Delay(TimeSpan.FromSeconds(3), ignoreTimeScale:false);
                }
            }

            ActiveUnit.Faction = 0;
            ActiveUnit.State = UnitStates.Idle;
            ActiveUnit.AfterPropertySet();
        }

        private void BuildMap()
        {
            Logger.Info("Starting build map ...");

            foreach (var data in tileOverlayDataSet)
            {
                TileOverlayDataMap.Add(data.Tile, data);
            }

            var cellBounds = TileMask.cellBounds;
            var sortingOrder = TileGround.GetComponent<TilemapRenderer>().sortingOrder;
            TileBounds = new TileBounds(cellBounds.min.x, cellBounds.min.y,
                cellBounds.max.x, cellBounds.max.y);

            for (var z = cellBounds.max.z; z >= cellBounds.min.z; z--)
            {
                for (var y = cellBounds.min.y; y < cellBounds.max.y; y++)
                {
                    for (var x = cellBounds.min.x; x < cellBounds.max.x; x++)
                    {
                        var tileLocation = new Vector3Int(x, y, z);

                        if (!TileMask.HasTile(tileLocation))
                        {
                            continue;
                        }

                        if (TileOverlayMap.ContainsKey(new Vector2Int(tileLocation.x, tileLocation.y)))
                        {
                            continue;
                        }

                        var tile = TileMask.GetTile(tileLocation);
                        var worldPosition = TileMask.GetCellCenterWorld(tileLocation);
                        var tileOverlay = TileOverlay.NewBuilder()
                            .SetTilePrefab(TileOverlayPrefab)
                            .SetTileContainer(TileOverlayContainer)
                            .SetTileLocation(tileLocation)
                            .SetWorldPosition(worldPosition)
                            .SetSortingOrder(sortingOrder)
                            .Build();

                        RegisterTileOverlay(tileLocation, tile, tileOverlay);
                    }
                }
            }

            HideTileMask();

            Logger.Info("Starting build map ... [OK]");
        }

        private void RegisterTileOverlay(Vector3Int tileLocation, TileBase tile, TileOverlay tileOverlay)
        {
            TileOverlayMap.Add(new Vector2Int(tileLocation.x, tileLocation.y), tileOverlay);
            TileOverlayDataMap.TryGetValue(tile, out tileOverlay.OverlayData);

            if (tileOverlay.OverlayData == null)
            {
                Logger.ErrorFormat("position:({0},{1},{2}) stub not found. tile:{3}",
                    tileLocation.x, tileLocation.y, tileLocation.x, tile.name);
            }
        }

        private void HideTileMask()
        {
            TileMask.gameObject.SetActive(false);
        }
    }
}