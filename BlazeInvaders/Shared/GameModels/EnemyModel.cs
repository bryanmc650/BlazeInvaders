using System;
using System.Collections.Generic;
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
    }
}
