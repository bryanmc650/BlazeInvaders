using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace BlazeInvaders.Shared.GameModels
{
    public enum EnemyType {Javascript, Angular, React }
    public enum EnemySpriteState {Open, Closed }
    public class EnemyModel : GameModelBase
    {
        public EnemyType EnemyType { get; set; }
        public EnemySpriteState SpriteState { get; set; }

        public override string SpriteName => $"Enemies\\{EnemyType}{SpriteState}";

        public override GameModelType ModelType => GameModelType.Enemy;

        public int PointValue
        { 
            get
            {
                if (EnemyType == EnemyType.Javascript)
                    return 30;
                else if (EnemyType == EnemyType.Angular)
                    return 20;
                else 
                    return 10;
            }
        }

        public override Rectangle CollisionRectangle
        {
            get
            {
                Rectangle r;

                if (EnemyType == EnemyType.React)
                {
                    int adjustmentX = (int)(Width * 0.2);
                    int adjustmentY = (int)(Height * 0.2);
                    r = new Rectangle(X + adjustmentX / 2, Y + adjustmentY / 2, Width - adjustmentX, Height - adjustmentY);
                }
                else r = new Rectangle(X,Y,(int)(Width * 0.9),(int)(Height * 0.9));

                return r;
            }
        }
    }
}
