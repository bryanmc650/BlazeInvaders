using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.JSInterop;

namespace BlazeInvaders.Client.Shared
{
    public class AudioController
    {

        public AudioController(ClientSideGameManager manager)
        {
            gameManager = manager;
        }
        private ClientSideGameManager gameManager;
        List<string> soundList { get; set; }
        Dictionary<string, string> soundDictionary { get; set; } = new Dictionary<string, string>();

        public async Task<bool> Initialize()
        {
            List<string> audioPaths = new List<string>();
            audioPaths.Add("GameAssets/Audio/Enemy/EnemyMove1.wav");
            audioPaths.Add("GameAssets/Audio/Enemy/EnemyMove2.wav");
            audioPaths.Add("GameAssets/Audio/Enemy/EnemyMove3.wav");
            audioPaths.Add("GameAssets/Audio/Enemy/EnemyMove4.wav");
            audioPaths.Add("GameAssets/Audio/Enemy/EnemyExplode.wav");
            audioPaths.Add("GameAssets/Audio/Player/DanosSnap.wav");
            audioPaths.Add("GameAssets/Audio/Player/Fire2.wav");
            audioPaths.Add("GameAssets/Audio/Player/PlayerExplode.wav");
            audioPaths.Add("GameAssets/Audio/GameOver.wav");
            foreach (var path in audioPaths)
                AddAudioMetaDataToDictionary(path);
            Console.WriteLine("invoking loadsounds");

            var result = await gameManager.JsRuntime.InvokeAsync<bool>("BlazeInvadersSound.loadSounds", soundDictionary);
            Console.WriteLine($"loadsounds invoked {result}");
            return result;
        }
        void AddAudioMetaDataToDictionary(string Path)
        {
            var audioFile = Path.Split('/').ToList().Last();
            soundDictionary.Add(audioFile, Path);
        }

        public async Task<bool> Play(string assetString)
        {
            return await gameManager.JsRuntime.InvokeAsync<bool>("BlazeInvadersSound.play", new object[] { assetString });
        }

        public async Task<bool> Pause(string assetString)
        {
            return await gameManager.JsRuntime.InvokeAsync<bool>("BlazeInvadersSound.pause", new object[] { assetString });
        }

        public async Task<bool> PauseAll()
        {
            foreach (var kvp in soundDictionary)
                await Pause(kvp.Key);
            return true;
        }

        public async void PlayEnemyExplode()
        {
            await Play("EnemyExplode.wav");
        }

        private int enemyMove = 1;

        public async void PlayEnemyMove()
        {
            await Play($"EnemyMove{enemyMove}.wav");

            if (enemyMove == 4)
                enemyMove = 1;
            else
                enemyMove++;
        }

        public async void PlayPlayerExplosion()
        {
            await Play("PlayerExplode.wav");
        }

        public async void PlayBulletFired()
        {
            await Play("Fire2.wav");
        }

        public async void StopBulletFired()
        {
            await Pause("Fire2.wav");
        }

        public async void PlayGameOver()
        {
            await Play("GameOver.wav");
        }

        public async void PlayDanosSnap()
        {
            await Play("DanosSnap.wav");
        }
    }
}
