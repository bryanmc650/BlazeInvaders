using BlazeInvaders.Shared.GameModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazeInvaders.Client.Shared
{
    public class ClientSideGameManager
    {
        System.Timers.Timer EngineTimer = new System.Timers.Timer();

        public ClientSideGameManager()
        {
            EngineTimer.Interval = 10;
            EngineTimer.Elapsed += engineTimerElapsed;
        }

        public int WindowWidth { get; set; }
        public int WindowHeight { get; set; }

        public List<GameModelBase> GameModels { get; set; } = new List<GameModelBase>();

        List<SaucerModel> GetSaucerModels() => GameModels.Where(x => x.ModelType == GameModelType.Saucer).Select(x => (SaucerModel)x).ToList();
        List<EnemyModel> GetEnemyModels() => GameModels.Where(x => x.ModelType == GameModelType.Enemy).Select(x => (EnemyModel)x).ToList();
        List<PlayerMissileModel> GetPlayerMissileModels() => GameModels.Where(x => x.ModelType == GameModelType.PlayerMissile).Select(x => (PlayerMissileModel)x).ToList();
        List<EnemyBombModel> GetEnemyBombModels() => GameModels.Where(x => x.ModelType == GameModelType.EnemyBomb).Select(x => (EnemyBombModel)x).ToList();
        List<ExplosionModel> GetExplosionsModels() => GameModels.Where(x => x.ModelType == GameModelType.Explosion).Select(x => (ExplosionModel)x).ToList();
        List<DanosSnapModel> GetDanosSnapModels() => GameModels.Where(x => x.ModelType == GameModelType.DanosSnap).Select(x => (DanosSnapModel)x).ToList();

        private void engineTimerElapsed(object sender, System.Timers.ElapsedEventArgs args)
        {
            EngineTimer.Stop();
            try
            {
                //Do Something
                //UpdateEnemies();
                //UpdatePlayers();
                //UpdateMissilies();
                //CeateRandomBombs();
                //CreateRandomSaucers();
                //CheckForPlayerMissileHits();
                //CheckForEnemyHits();
                //UpdateExplosions();
                //UpdateGameStatus();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            EngineTimer.Start();
            
        }

    }
    
}
