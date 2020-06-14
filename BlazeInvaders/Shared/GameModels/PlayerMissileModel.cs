using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace BlazeInvaders.Shared.GameModels
{
    public class PlayerMissileModel : GameModelBase
    {
        public override string SpriteName => "Players\\PlayerMissile";
        public override GameModelType ModelType => GameModelType.PlayerMissile;

        public override Rectangle CollisionRectangle
        {
            get 
            {
                var r = new Rectangle(X,Y,(int)(Width * 0.27),(int)(Height * 0.4));
                return r;
            }
        }
    }
}
