using System;
using System.Collections.Generic;
using System.Text;

namespace BlazeInvaders.Shared.GameModels
{
    public class SaucerModel : GameModelBase
    {
        public int PointValue { get; set; }

        public override string SpriteName => "Enemies\\Saucer";
        public override GameModelType ModelType => GameModelType.Saucer;
    }
}
