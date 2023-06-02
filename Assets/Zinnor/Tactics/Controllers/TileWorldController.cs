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

        public GameObject UnitContainer;
        public Unit UnitPrefab;
        public ScriptableWeapon UnitWeapon;

        public void Awake()
        {
            DoBuildMap();
        }

        public void Start()
        {
            TestMovementSquares().Forget();
        }

        private async UniTask TestMovementSquares()
        {
            var location = new Vector2Int(-10, -8);
            var extra = new Stats();
            var tile = TileOverlayMap[location];
            ActiveUnit = Unit.newBuilder()
                .SetId(1)
                .SetPrefab(UnitPrefab)
                .SetParent(UnitContainer)
                .SetWeapon(UnitWeapon)
                .SetExtra(extra)
                .SetTile(tile)
                .SetSortingOrder(1)
                .Build();

            var movableTiles = MoveFinder.Find(ActiveUnit, tile, TileOverlayMap);
            TileOverlayUtils.ShowMoveSquares(ActiveUnit, tile, movableTiles.Values.ToList());

            var attackableTiles = AttackFinder.Find(ActiveUnit, tile, movableTiles, TileOverlayMap);
            TileOverlayUtils.ShowAttackSquares(ActiveUnit, attackableTiles.Values.ToList());

            var boundsTiles = BoundsFinder.Find(movableTiles.Values,
                p => movableTiles.ContainsKey(p),
                p => TileOverlayMap.ContainsKey(p));

            if (boundsTiles.Count != 0)
            {
                var end = boundsTiles[Random.Range(0, boundsTiles.Count)];
                var pathTiles = PathFinder.AStarSearch(ActiveUnit, tile, end, movableTiles);
                TileOverlayUtils.ShowMoveArrows(ActiveUnit, tile, pathTiles);
            }

            for (int i = 0; i < 2; ++i)
            {
                ActiveUnit.Faction = i;
                ActiveUnit.AfterControllerChanged();
                
                foreach (UnitStates state in Enum.GetValues(typeof(UnitStates)))
                {
                    await UniTask.Delay(TimeSpan.FromSeconds(3), ignoreTimeScale:false);
                    ActiveUnit.State = state;
                    ActiveUnit.AfterStateChanged();
                }
            }
        }

        private void DoBuildMap()
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

                        RegisterTileStub(tileLocation, tile, tileOverlay);
                    }
                }
            }

            HideTileMask();

            Logger.Info("Starting build map ... [OK]");
        }

        private void RegisterTileStub(Vector3Int tileLocation, TileBase tile, TileOverlay tileOverlay)
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

        public void ClearTiles()
        {
            foreach (var item in TileOverlayMap)
            {
                item.Value.HideOverlaySprite();
            }
        }
    }
}