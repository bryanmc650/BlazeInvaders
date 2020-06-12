using System;
using System.Collections.Generic;
using System.Text;

namespace BlazeInvaders.Shared.GameModels
{
    public class ExplosionModel : GameModelBase
    {
        public int ExplosionState { get; set; } = 1;
        public override GameModelType ModelType => GameModelType.Explosion;

        public override string SpriteName => $"Enemies\\Explode{ExplosionState}";
        public int PointValue { get; set; }
    }
}
