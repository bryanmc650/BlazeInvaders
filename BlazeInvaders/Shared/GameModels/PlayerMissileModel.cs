using System;
using System.Collections.Generic;
using System.Text;

namespace BlazeInvaders.Shared.GameModels
{
    public class PlayerMissileModel : GameModelBase
    {
        public override string SpriteName => "Players\\PlayerMissile";
        public override GameModelType ModelType => GameModelType.PlayerMissile;
    }
}
