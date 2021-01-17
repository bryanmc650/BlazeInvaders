using System;
using System.Collections.Generic;
using System.Text;

namespace BlazeInvaders.Shared.GameModels
{
    public enum ThanosSnapState {Rising, ReadyToKill, Killing, Retracting }
    public class ThanosSnapModel : GameModelBase
    {
        public DateTime ThanosSnapTime { get; set; }
        public ThanosSnapState SnapState { get; set; }

        public override string SpriteName => "Players\\DanosSnap";
        public override GameModelType ModelType => GameModelType.ThanosSnap;
    }
}
