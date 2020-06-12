using System;
using System.Collections.Generic;
using System.Text;

namespace BlazeInvaders.Shared.GameModels
{
    public class GameOverModel : GameModelBase
    {
        public override string SpriteName => "Enemies\\GameOver";
        public override GameModelType ModelType => GameModelType.GameOver;

        public DateTime GameOverTime { get; set; }
    }
}
