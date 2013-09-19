using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace floodControl
{
    class RotatingPiece : GamePiece
    {
        public bool clockwise;
        public static float rotationRate = (MathHelper.PiOver2 / 10);
        private float rotationAmount = 0;
        public int rotationRemaining = 10;

        public float RotationAmount
        {
            get
            {
                if (clockwise)
                    return rotationAmount;
                else
                    return (MathHelper.Pi * 2) - rotationAmount;
            }
        }

        public RotatingPiece(string type, bool clockwise) :
            base(type)
        {
            this.clockwise = clockwise;
        }

        public void Update()
        {
            rotationAmount += rotationRate;
            rotationRemaining = (int)MathHelper.Max(0, rotationRemaining - 1);
        }
    }
}
