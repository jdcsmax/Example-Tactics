using UnityEngine;
using Zinnor.Tactics.Tiles;

namespace Zinnor.Tactics.Scriptables.Restrictions
{
    /// <summary>
    /// 限制
    /// </summary>
    [CreateAssetMenu(fileName = "ScriptableRestriction", menuName = "ScriptableObjects/ScriptableRestriction")]
    public class ScriptableRestriction : ScriptableObject
    {
        /// <summary>
        /// 文本描述
        /// </summary>
        public string Message;

        /// <summary>
        /// 格子数据
        /// </summary>
        public TileOverlayData OverlayData;

        /// <summary>
        /// 移动消耗
        /// </summary>
        public int MoveCost;
    }
}