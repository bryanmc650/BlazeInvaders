using System;
using System.Collections.Generic;
using System.Text;

namespace BlazeInvaders.Shared.GameModels
{
    public class LifeLostModel : GameModelBase
    {
        public DateTime LifeLostTime { get; set; }
        public override string SpriteName => "Enemies\\LifeLost";
        public override GameModelType ModelType => GameModelType.LifeLost;
    }
}
