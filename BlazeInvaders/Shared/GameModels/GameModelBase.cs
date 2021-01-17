using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace BlazeInvaders.Shared.GameModels
{
    public enum GameModelType {None, Enemy, Player, PlayerMissile,EnemyBomb, Saucer, Explosion, ThanosSnap, LifeLost, RoundCompleted, GameOver}
    public class GameModelBase
    {
        public virtual GameModelType ModelType => GameModelType.None;
        public int X { get; set; }
        public int Y { get; set; }
        public int Height { get; set; }
        public int Width { get; set; }

        public virtual string SpriteName
        {
            get { return ""; }
        }

        public virtual Rectangle CollisionRectangle
        {
            get 
            {
                return new Rectangle(X,Y,Width,Height);
            }
        }

        public virtual string TextElement => null;
    }
}
