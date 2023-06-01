using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Zinnor.Tactics.Navigation;
using UnityEngine;
using UnityEngine.Tilemaps;
using Zinnor.Tactics.Navigation;
using Zinnor.Tactics.Scriptables.Weapons;
using Zinnor.Tactics.Tiles;
using Zinnor.Tactics.Units;

namespace Zinnors.Tactics.Controllers
{
    public class TileWorldController : MonoBehaviour
    {
        public TileOverlay TileOverlayPrefab;
        public GameObject TileOverlayContainer;
        public ScriptableTileSet ScriptableTileSet;
        public Tilemap TileMask;
        public Tilemap TileGround;
        public BoundsInt TileBounds;
        public Dictionary<TileBase, ScriptableTile> TileDataMap;
        public Dictionary<Vector2Int, TileOverlay> TileOverlayMap;

        public Unit ActiveUnit;

        public Unit UnitPrefab;
        public GameObject UnitContainer;
        public ScriptableClass UnitClass;
        public ScriptableWeapon UnitWeapon;

        public void Awake()
        {
            PreBuildMap();
            DoBuildMap();
            PostBuildMap();
        }

        public void Start()
        {
            ClearTiles();

            TestMovementSquares();
        }

        private Unit CreateUnit(ScriptableClass clazz, Stats stats, ScriptableWeapon weapon)
        {
            var unit = Instantiate(UnitPrefab, UnitContainer.transform);

            unit.Class = clazz;
            unit.Stats = stats;
            unit.Weapon = weapon;

            unit.HP = unit.MaxHP;

            return unit;
        }

        private void TestMovementSquares()
        {
            var unit = CreateUnit(UnitClass, new Stats(), UnitWeapon);
            var location = new Vector2Int(-10, -10);
            var start = TileOverlayMap[location];

            var movableTiles = MoveFinder.Find(unit, start, TileOverlayMap);
            TileOverlayUtils.ShowMoveSquares(unit, start, movableTiles.Values.ToList());

            var attackableTiles = AttackFinder.Find(unit, start, movableTiles, TileOverlayMap);
            TileOverlayUtils.ShowAttackSquares(unit, attackableTiles.Values.ToList());

            var boundsTiles = BoundsFinder.Find(movableTiles.Values,
                p => movableTiles.ContainsKey(p),
                p => TileOverlayMap.ContainsKey(p));

            if (boundsTiles.Count > 0)
            {
                var end = boundsTiles[UnityEngine.Random.Range(0, boundsTiles.Count)];
                var pathTiles = PathFinder.AStarSearch(unit, start, end, movableTiles);
                TileOverlayUtils.ShowMoveArrows(unit, start, pathTiles);
            }
        }

        protected void PreBuildMap()
        {
            TileDataMap = new Dictionary<TileBase, ScriptableTile>();
            TileOverlayMap = new Dictionary<Vector2Int, TileOverlay>();

            foreach (var data in ScriptableTileSet)
            {
                TileDataMap.Add(data.Tile, data);
            }

            TileBounds = TileMask.cellBounds;
        }

        protected void DoBuildMap()
        {
            var sortingOrder = TileGround.GetComponent<TilemapRenderer>().sortingOrder;

            for (int z = TileBounds.max.z; z >= TileBounds.min.z; z--)
            {
                for (int y = TileBounds.min.y; y < TileBounds.max.y; y++)
                {
                    for (int x = TileBounds.min.x; x < TileBounds.max.x; x++)
                    {
                        var tileLocation3D = new Vector3Int(x, y, z);
                        var tileLocation2D = new Vector2Int(x, y);

                        if (TileMask.HasTile(tileLocation3D) && !TileOverlayMap.ContainsKey(tileLocation2D))
                        {
                            var tile = TileMask.GetTile(tileLocation3D);
                            var tileOverlay = Instantiate(TileOverlayPrefab, TileOverlayContainer.transform);
                            var cellWorldPosition = TileMask.GetCellCenterWorld(tileLocation3D);
                            tileOverlay.transform.position = new Vector3(
                                cellWorldPosition.x, cellWorldPosition.y, cellWorldPosition.z + 1);
                            tileOverlay.TileSpriteRenderer.sortingOrder = sortingOrder;
                            tileOverlay.Grid3DLocation = tileLocation3D;

                            TileDataMap.TryGetValue(tile, out tileOverlay.Data);

                            TileOverlayMap.Add(tileLocation2D, tileOverlay);
                        }
                    }
                }
            }
        }

        protected void PostBuildMap()
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