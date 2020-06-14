using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace BlazeInvaders.Shared.GameModels
{
    public class PlayerModel : GameModelBase
    {
        public override string SpriteName => "Players\\"+ ( Velocity != 0 ? "PlayerMoving" : "PlayerIdle");
        public int Velocity { get; set; }
        public override GameModelType ModelType => GameModelType.Player;

        public HashSet<ConsoleKey> KeyPressHistory { get; set; } = new HashSet<ConsoleKey>();

        public override Rectangle CollisionRectangle
        {
            get 
            {
                int heightAdjustment = (int)(Height * .4);
                var r = new Rectangle(X, Y + heightAdjustment / 2, Width, Height - heightAdjustment);
                return r;
            }
        }
    }
}
