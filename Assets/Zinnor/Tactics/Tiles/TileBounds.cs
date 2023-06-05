﻿namespace Zinnor.Tactics.Tiles
{
    public struct TileBounds
    {
        public int xMin;
        public int yMin;
        public int xMax;
        public int yMax;

        public TileBounds(int xMin, int yMin, int xMax, int yMax)
        {
            this.xMin = xMin;
            this.yMin = yMin;
            this.xMax = xMax;
            this.yMax = yMax;
        }
    }
}