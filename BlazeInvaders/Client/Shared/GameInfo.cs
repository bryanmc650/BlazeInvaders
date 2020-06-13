using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazeInvaders.Client.Shared
{
    public class GameInfo
    {
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int Score { get; set; }
        public int Lives { get; set; }
        public int SaucersSinceLastDanosSnap { get; set; } = 0;
        public int DanosSnaps { get; set; } = 0;
        public int Round { get; set; }
    }
}
