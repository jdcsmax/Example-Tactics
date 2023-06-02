using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Zinnor.Tactics.Scriptables.Effects;

namespace Zinnor.Tactics.Tiles
{
    [CreateAssetMenu(fileName = "TileOverlayData", menuName = "ScriptableObjects/TileOverlayData")]
    public class TileOverlayData : ScriptableObject
    {
        /// <summary>
        /// 对应的瓦片实例
        /// </summary>
        public TileBase Tile;

        /// <summary>
        /// 格子的效果列表
        /// </summary>
        public List<ScriptableEffect> Effects = new();

        /// <summary>
        /// 文本信息
        /// </summary>
        public string Text;

        /// <summary>
        /// 可否步行
        /// </summary>
        public bool Walkable;

        /// <summary>
        /// 可否飞行
        /// </summary>
        public bool Flyable;

        /// <summary>
        /// 可否通过
        /// </summary>
        public bool Traverable => Walkable || Flyable;
    }
}