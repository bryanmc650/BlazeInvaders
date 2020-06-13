using BlazeInvaders.Shared.GameModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace BlazeInvaders.Client.Shared
{
    enum EnemyDirection { Right, Left }
    public class ClientSideGameManager
    {
        public GameInfo GameInfo { get; set; } = new GameInfo(); 
        Random random = new Random();
        EnemyDirection enemyDirection;
        System.Timers.Timer EngineTimer = new System.Timers.Timer();
        private DateTime lastEnemyUpdateTime;

        public Action AfterGameUpdated { get; set; }

        public ClientSideGameManager()
        {
            EngineTimer.Interval = 10;
            EngineTimer.Elapsed += engineTimerElapsed;
        }

        public int WindowWidth { get; set; } = 525;
        public int WindowHeight { get; set; } = 600;

        public List<GameModelBase> GameModels { get; set; } = new List<GameModelBase>();

        List<SaucerModel> GetSaucerModels() => GameModels.Where(x => x.ModelType == GameModelType.Saucer).Select(x => (SaucerModel)x).ToList();
        List<EnemyModel> GetEnemyModels() => GameModels.Where(x => x.ModelType == GameModelType.Enemy).Select(x => (EnemyModel)x).ToList();
        List<PlayerMissileModel> GetPlayerMissileModels() => GameModels.Where(x => x.ModelType == GameModelType.PlayerMissile).Select(x => (PlayerMissileModel)x).ToList();
        List<EnemyBombModel> GetEnemyBombModels() => GameModels.Where(x => x.ModelType == GameModelType.EnemyBomb).Select(x => (EnemyBombModel)x).ToList();
        List<ExplosionModel> GetExplosionsModels() => GameModels.Where(x => x.ModelType == GameModelType.Explosion).Select(x => (ExplosionModel)x).ToList();
        List<DanosSnapModel> GetDanosSnapModels() => GameModels.Where(x => x.ModelType == GameModelType.DanosSnap).Select(x => (DanosSnapModel)x).ToList();

        //Helper method for random chance.
        bool GetRandomChance(int outOf)
        {
            return random.Next(outOf) == 0;
        }

        public void StartNewGame()
        {
            EngineTimer.Stop();
            GameModels.Clear();

            GameInfo = new GameInfo();
            GameInfo.StartTime = DateTime.Now;
            GameInfo.Lives = 3;
            GameInfo.Round = 1;

            CreateEnemies();
            EngineTimer.Start();
        }

        void CreateEnemies()
        {
            int enemiesPerRow = 11;
            int xGap = 42;
            int yGap = 35;

            List<EnemyType> enemyRowType = new List<EnemyType> {EnemyType.Javascript, EnemyType.Angular, EnemyType.Angular, EnemyType.React, EnemyType.React };

            int currentY = 50;
            foreach (var rowType in enemyRowType)
            {
                int currentX = 3;
                for (int i = 1; i < enemiesPerRow; i++)
                {
                    GameModels.Add(new EnemyModel() { EnemyType = rowType, X = currentX, Y = currentY, Height = 30, Width = 30 });
                    currentX += xGap;
                }
                currentY += yGap;
            }
        }

        void UpdateEnemies()
        {
            var enemyModels = GetEnemyModels();
            double ellapsed = (DateTime.Now - lastEnemyUpdateTime).TotalMilliseconds;

            //We want the enemies to get a little faster each round - and a lot faster when there are few enemies left. 
            int stepSize = 10; // + (GameInfo.Round / 3); //gradually increase step size.
            int threshhold = 700; // - (GameInfo.Round * 30); //start off slowly. 
            int enemyCount = enemyModels.Count();

            //The speed of the enemies is reflected by threshhold, depending on the number of enemies we update the value.
            if (enemyCount == 1)
            {
                threshhold -= 300;
                stepSize = 13;
            }
            else if (enemyCount == 1)
                threshhold -= 200;
            else if (enemyCount == 3)
                threshhold -= 150;
            else if (enemyCount == 10)
                threshhold -= 100;
            else if (enemyCount == 20)
                threshhold -= 75;
            else if (enemyCount == 30)
                threshhold -= 50;

            if (ellapsed < threshhold)
                return;

            lastEnemyUpdateTime = DateTime.Now;
            bool newRow = false;

            //Check if a change of direction is required. If so we will need to change direction and a newRow so that is set to true. 
            //Check is done by measuring if the new position will be beyond the window width.
            if (enemyDirection == EnemyDirection.Right && enemyModels.Max(x=> x.X + x.Width) + stepSize > WindowWidth)
            {
                enemyDirection = EnemyDirection.Left;
                newRow = true;
            }
            else if (enemyDirection == EnemyDirection.Left && enemyModels.Min(x=> x.X) - stepSize < 0)
            {
                enemyDirection = EnemyDirection.Right;
                newRow = true;
            }

            if (newRow) //If new row is required we drop Y values down and toggle sprite state.
            {
                foreach (var model in enemyModels)
                {
                    model.Y += 20;
                    model.SpriteState = model.SpriteState == EnemySpriteState.Closed ? EnemySpriteState.Open : EnemySpriteState.Closed;
                }
            }
            else //New row not required, we move X position by delta X to the left or right and toggle sprite state.
            {
                int deltaX = enemyDirection == EnemyDirection.Right ? stepSize : -stepSize;

                foreach (var model in enemyModels)
                {
                    model.X += deltaX;
                    //Console.WriteLine($"enemy updated by {deltaX}"); (Debug Output)
                    model.SpriteState = model.SpriteState == EnemySpriteState.Closed ? EnemySpriteState.Open : EnemySpriteState.Closed;
                }
            
            }

        }

        

        private void engineTimerElapsed(object sender, System.Timers.ElapsedEventArgs args)
        {
            EngineTimer.Stop();
            try
            {
                //Do Something
                UpdateEnemies();
                //UpdatePlayers();
                //UpdateMissilies();
                CreateRandomBombs();
                CreateRandomSaucers();
                UpdateEnemyBombs();
                //CheckForPlayerMissileHits();
                //CheckForEnemyHits();
                //UpdateExplosions();
                //UpdateGameStatus();
                UpdateSaucers();

                if (AfterGameUpdated is object) //Check the event is not NULL.
                {
                    AfterGameUpdated();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            EngineTimer.Start();
            
        }

        private void CreateRandomSaucers()
        {
            if (GameModels.Any(x => x.ModelType == GameModelType.Saucer)) //only 1 saucer at a time.
                return;

            if (!GetRandomChance(300)) // 1 in 300 chance of generating a saucer.
                return;

            SaucerModel saucer = new SaucerModel(); //generate a saucer.
            saucer.X = 0;
            saucer.Y = 10;
            saucer.Height = 35;
            saucer.Width = 45;
            saucer.PointValue = random.Next(20, 100);
            GameModels.Add(saucer);
        }

        void UpdateEnemyBombs()
        {
            int stepSize = 3;
            var enemyBombModels = GetEnemyBombModels(); //Get the list of enemy bombs.

            foreach (var model in enemyBombModels.ToList())
            {
                model.Y += stepSize; //Increase Y pos by stepsize and remove when off the screen.
                if (model.Y > WindowHeight + model.Height)
                    GameModels.Remove(model);
            }
        }

        void UpdateSaucers()
        {
            int stepSize = 1;
            var saucerModels = GetSaucerModels();

            foreach (var model in saucerModels.ToList())
            {
                model.X += stepSize;
                if (model.X > WindowWidth + model.Width)
                    GameModels.Remove(model);
            }
        }

        private void CreateRandomBombs()
        {
            int chance = 80 - GameInfo.Round; //Initial 1 in 80 chance which increases with round. 
            if (chance < 10) //Chance limit of 10.
                chance = 10;

            if (!GetRandomChance(chance)) //Check if we should create a bomb or not. Return if not. 
                return;

            var enemyModels = GetEnemyModels(); //Check that we have some enemy models to attach the bomb to. 
            if (enemyModels.Count() == 0)
                return;

            EnemyModel enemy = enemyModels[random.Next(enemyModels.Count())]; //Select a random Enemy to assign the bomb to.

            //Create a bomb and set the X and Y to that of the chosen enemy model.
            EnemyBombModel enemyBomb = new EnemyBombModel();
            enemyBomb.X = enemy.X + enemy.Width / 2;
            enemyBomb.Y = enemy.Y;
            enemyBomb.Height = 40;
            enemyBomb.Width = 30;

            GameModels.Add(enemyBomb);
        }

    }
    
}
