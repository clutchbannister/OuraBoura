using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using Nez;
using Nez.Tweens;
using ouraboura.Src.Scenes;

namespace ouraboura.Src
{
    class gameManager
    {
        private static gameManager instance;
        public int currentScene { get; set; }
        public int nextScene { get; set; }
        public bool gameOver = false;

        public List<SoundEffect> SoundEffects;
        public List<Song> Songs;
        public int volumeLevel, returnVolume;
        public bool audio, pause;
        public float audioVolume = 1.0f;

        //public const int BackGroundRenderLayer = 10;
        //public const int GameRenderLayer = 1;
        //public const int UIRenderLayer = 2;       
        //public const int MenuRenderLayer = 3;

        public enum GameScenes
        {
            splashScene,
            titleScene,
            menuScene,
            gameScene,
            gameOverScene
        }

        public enum Transitions
        {
            fade,
            cross,
            squares
        }

        public enum VolumeLevels
        {
            off,
            low,
            medium,
            high,
            full,
            reset
        }

        public static gameManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new gameManager();
                }
                return instance;
            }
        }

        public void Initialize()
        {

            SoundEffects = new List<SoundEffect>();
            Songs = new List<Song>();

            try
            {
                SoundEffects.Add(Core.content.Load<SoundEffect>("sound/testSound"));
                Songs.Add(Core.content.Load<Song>("sound/introSong"));
                Songs.Add(Core.content.Load<Song>("sound/mainSong"));

            }
            catch (Exception)
            {

                Debug.log("Cannot find files");
            }

            Core.scene = new splashScene();
            Core.pauseOnFocusLost = true;
            Core.debugRenderEnabled = true;

            audio = true;
            pause = false;
            volumeLevel = (int)VolumeLevels.reset;
            SetReturnVolume(100);
        }

        IEnumerator TransitionScene(Scene scene, Transitions transition)
        {

            TweenManager.stopAllTweens();
            yield return Coroutine.waitForSeconds(0.4f);
            switch (transition)
            {
                case Transitions.fade:
                    Core.startSceneTransition(new FadeTransition(() => scene));
                    break;
                case Transitions.cross:
                    Core.startSceneTransition(new CrossFadeTransition(() => scene));
                    break;
                case Transitions.squares:
                    Core.startSceneTransition(new SquaresTransition(() => scene));
                    break;
                default:
                    Core.startSceneTransition(new FadeTransition(() => scene));
                    break;
            }
        }

        public void LoadSceneWithTransition(Transitions transitionType, Scene scene)
        {
            Core.startCoroutine(TransitionScene(scene, transitionType));
        }

        public string AudioOff()
        {
            string returnString;

            if (!audio)
            {
                SoundEffect.MasterVolume = 1.0f;
                MediaPlayer.Volume = 1.0f;
                returnString = "On";
            }
            else
            {
                SoundEffect.MasterVolume = 0.0f;
                MediaPlayer.Volume = 0.0f;
                returnString = "Off";
            }
            audio = !audio;
            return returnString;
        }

        public string AudioVolume()
        {
            if (volumeLevel == (int)VolumeLevels.reset)
                volumeLevel = (int)VolumeLevels.off;

            switch (volumeLevel)
            {
                case (int)VolumeLevels.off:
                    SetReturnVolume(0);
                    audioVolume = 0.0f;
                    break;
                case (int)VolumeLevels.low:
                    SetReturnVolume(20);
                    audioVolume = 0.2f;
                    break;
                case (int)VolumeLevels.medium:
                    SetReturnVolume(40);
                    audioVolume = 0.4f;
                    break;
                case (int)VolumeLevels.high:
                    SetReturnVolume(60);
                    audioVolume = 0.6f;
                    break;
                case (int)VolumeLevels.full:
                    SetReturnVolume(100);
                    audioVolume = 1.0f;
                    break;
                case (int)VolumeLevels.reset:
                    SetReturnVolume(100);
                    audioVolume = 1.0f;
                    break;
                default:
                    SetReturnVolume(100);
                    audioVolume = 1.0f;
                    break;
            }

            volumeLevel++;

            SoundEffect.MasterVolume = audioVolume;
            MediaPlayer.Volume = audioVolume;
            return GetReturnVolume().ToString();
        }

        public void SetReturnVolume(int volume)
        {
            returnVolume = volume;
        }

        public int GetReturnVolume()
        {
            return returnVolume;
        }


    }
}
