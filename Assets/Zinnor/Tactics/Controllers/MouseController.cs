using UnityEngine;
using Zinnor.Tactics.Scriptables.Events;
using Zinnor.Tactics.Tiles;

namespace Zinnor.Tactics.Controllers
{
    public class MouseController : MonoBehaviour
    {
        public Camera MainCamera;
        public ScriptableObservable MousePressed;
        public GameObjectObservable FocusedOnTileChanged;
        public TileOverlay FocusedOnTile;
        public TileWorldController WorldController;

        private void Start()
        {
            FocusedOnTile = WorldController.GetOverlayTileByWorldPosition(transform.position);

            if (FocusedOnTileChanged != null)
            {
                FocusedOnTileChanged.Emit(FocusedOnTile.gameObject);
            }
        }

        private void Update()
        {
            if (Input.GetMouseButtonUp(0) && MousePressed)
            {
                MousePressed.Emit();
            }
        }

        private void FixedUpdate()
        {
            SetFocusedOnTile(GetFocusedOnTile2D(GetMouseWorldPosition()));
        }

        private Vector3 GetMouseWorldPosition()
        {
            return MainCamera.ScreenToWorldPoint(Input.mousePosition);
        }

        private TileOverlay GetFocusedOnTile2D(Vector3 mousePos)
        {
            return WorldController.GetOverlayTileByWorldPosition(mousePos);
        }

        private void SetFocusedOnTile(TileOverlay newFocusedOnTile)
        {
            if (newFocusedOnTile == null)
            {
                return;
            }

            if (FocusedOnTile == newFocusedOnTile)
            {
                return;
            }

            FocusedOnTile = newFocusedOnTile;
            transform.position = newFocusedOnTile.transform.position;

            if (FocusedOnTileChanged)
            {
                FocusedOnTileChanged.Emit(FocusedOnTile.gameObject);
            }
        }

        // private static TileOverlay GetFocusedOnTile2D(Vector3 mousePos)
        // {
        //     // By passing Vector2.zero as the direction, the cast will simply have no direction.
        //     // In turn, the distance you pass to the function doesn't matter. You can set it to anything.
        //     // Distance is a scalar that gets multiplied with the direction vector. If the direction is zero,
        //     // then the cast will be zero as well, i.e <0,0>*distance = <0,0>.
        //
        //     // In consequence, the cast will simply start and finish at the origin. When you click directly on the collider,
        //     // you set the origin to your mouse's position (converted to world space). Because the origin is positioned within
        //     // the collider, it detects a hit. Even if no ray was cast over a direction and distance. The ray cast in this case
        //     // consists of simply one point (origin).
        //
        //     // As an experiment, set the direction to Vector2.right and then click to the left of the collider.
        //     // You'll see that it gets hit. Why? Because the ray originates from the left and gets cast to the right
        //     // until it hits the collider. Same thing would happen with Vector2.up and clicking below the collider.
        //
        //     var origin = new Vector2(mousePos.x, mousePos.y);
        //     var hit = Physics2D.Raycast(origin, Vector2.zero);
        //     return hit.collider ? hit.collider.gameObject.GetComponent<TileOverlay>() : null;
        // }
    }
}