using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace floodControl
{
    public class FalliingPiece : GamePiece
    {
        public int offset;
        public static int fallRate = 5;

        public FalliingPiece(string type, int offset) : base(type)
        {
            this.offset = offset;
        }

        public void Update()
        {
            offset = (int)MathHelper.Max(0, offset - fallRate);
        }
    }
}
