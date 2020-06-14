using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace BlazeInvaders.Shared.GameModels
{
    public class EnemyBombModel : GameModelBase
    {
        public override string SpriteName => "Enemies\\EnemyBomb";

        public override GameModelType ModelType => GameModelType.EnemyBomb;

        public override Rectangle CollisionRectangle
        {
            get
            {
                var r = new Rectangle(X,Y,(int)(Width * 0.3),(int)(Height * 0.45));
                return r;
            }
        }
    }
}
