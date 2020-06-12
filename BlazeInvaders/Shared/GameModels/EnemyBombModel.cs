using System;
using System.Collections.Generic;
using System.Text;

namespace BlazeInvaders.Shared.GameModels
{
    public class EnemyBombModel : GameModelBase
    {
        public override string SpriteName => "Enemies\\EnemyBomb";

        public override GameModelType ModelType => GameModelType.EnemyBomb;
    }
}
