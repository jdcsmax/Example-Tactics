using UnityEngine;
using Zinnor.Tactics.Scriptables;

namespace Zinnor.Tactics.Tiles
{
    [CreateAssetMenu(fileName = "ScriptableTileSet", menuName = "ScriptableObjects/ScriptableTileSet")]
    public class ScriptableTileSet : ScriptableSet<ScriptableTile>
    {
    }
}