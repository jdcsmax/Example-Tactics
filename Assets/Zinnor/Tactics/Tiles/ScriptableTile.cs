using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Zinnor.Tactics.Scriptables;

namespace Zinnor.Tactics.Tiles
{
    [CreateAssetMenu(fileName = "ScriptableTile", menuName = "ScriptableObjects/ScriptableTile")]
    public class ScriptableTile : ScriptableObject
    {   
        /**
         * 数据关联瓦片
         */
        public TileBase Tile;

        /**
         * 对象文本信息
         */
        public string Text;

        /**
         * 可否步行
         */
        public bool Walkable;

        /**
         * 可否飞行
         */
        public bool Flyable;

        /**
         * 关联效果列表
         */
        public List<ScriptableEffect> Effects = new List<ScriptableEffect>();
    }
}