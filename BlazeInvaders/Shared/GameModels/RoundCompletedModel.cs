using System;
using System.Collections.Generic;
using System.Text;

namespace BlazeInvaders.Shared.GameModels
{
    public class RoundCompletedModel : GameModelBase
    {
        public DateTime RoundCompletedTime { get; set; }
        public override string SpriteName => "Enemies\\RoundCompleted";

        public override GameModelType ModelType => GameModelType.RoundCompleted;
    }
}
