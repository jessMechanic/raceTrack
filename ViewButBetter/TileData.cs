using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace ViewButBetter
{
    internal class TileData
    {
        public Vector2 position;
        public Vector2 outerLane;
        public Vector2 innerLane;
        public string sprite;

        public Vector2 OuterAbsolute { get { return (position * 32) + outerLane; } }
        public Vector2 InnerAbsolute { get { return (position * 32) + innerLane; } }
        public TileData(string sprite,Vector2 position, Vector2 outerLane, Vector2 innerLane)
        {
            this.sprite = sprite;
            this.position = position;
            this.outerLane = outerLane;
            this.innerLane = innerLane;
        }
    }
}
