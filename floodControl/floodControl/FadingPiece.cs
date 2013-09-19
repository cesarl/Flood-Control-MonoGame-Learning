using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace floodControl
{
    class FadingPiece : GamePiece
    {
        public float alphaLevel = 1.0f;
        public static float rate = 0.02f;

        public FadingPiece(string type, string suffix) :
            base(type, suffix)
        { }

        public void Update()
        {
            alphaLevel - MathHelper.Max(0, alphaLevel - rate);
        }
    }
}
