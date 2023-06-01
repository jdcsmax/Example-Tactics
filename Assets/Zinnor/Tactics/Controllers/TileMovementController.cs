using System.Collections.Generic;
using Assets.Zinnor.Tactics.Navigation;
using UnityEngine;
using Zinnor.Tactics.Units;

namespace Zinnors.Tactics.Controllers
{
    public class TileMovementController : MonoBehaviour
    {
        public float speed;
        public Unit activeUnit;
        public bool enableAutoMove;
        public bool showAttackRange;
        public bool moveThroughAllies = true;
    }
}