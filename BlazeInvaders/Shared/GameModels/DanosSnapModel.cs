using System;
using System.Collections.Generic;
using System.Text;

namespace BlazeInvaders.Shared.GameModels
{
    public enum DanosSnapState {Rising, ReadyToKill, Killing, Retracting }
    public class DanosSnapModel : GameModelBase
    {
        public DateTime DanosSnapTime { get; set; }
        public DanosSnapState SnapState { get; set; }

        public override string SpriteName => "Player\\DanosSnap";
        public override GameModelType ModelType => GameModelType.DanosSnap;
    }
}
