using System;
using System.Collections;
using System.Diagnostics;
using Athena.Common;
using Athena.Common.UI;
using Managers;
using UIControllers;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace AppFlow
{
    public class AppStateGameplay : IState
    {
        public bool IsInitialized => _isInitialized;
        private bool _isInitialized = false;

        public int LevelId;


        private UIControllers.LevelWinUI _levelWinUI;
        private UIControllers.LevelLoseUI _levelLoseUI;
        private UIControllers.LevelOutOfMoveUI _levelOutOfMoveUI;
        private UIControllers.LevelGiveUpUI _levelGiveUpUI;
        private UIControllers.ActionPhaseUI _actionPhaseUI;
        private UIControllers.CheatUI _cheatUI;
        private UIControllers.PauseUI _pauseUI;

        #region App States
        public void Initialize()
        {
            SceneManager.sceneLoaded += onSceneLoaded;
            SceneManager.LoadScene(C.Scenes.Game, LoadSceneMode.Single);
            
        }

        private void onSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            SceneManager.sceneLoaded -= onSceneLoaded;
            _isInitialized = true;
            var root = scene.GetRootGameObjects()[0];
            showUIActionPhase();
        }

        public void Resume()
        {

        }

        public void Clear()
        {
        }

        public void Exit()
        {
            // _gameplayMonoBehaviour.ResetAll();
        }
        #endregion App States

        private void showUIActionPhase()
        {
            _actionPhaseUI = UIManager.Instance.ShowUIOnTop<UIControllers.ActionPhaseUI>(C.Layer.ActionPhase);
            _actionPhaseUI.OnPause += onPauseClicked;
        }

        private void hideUIActionPhase()
        {
            UIManager.Instance.ReleaseUI(_actionPhaseUI, true);
            _actionPhaseUI.OnPause -= onPauseClicked;
        }


        private void onPauseClicked()
        {
            UIManager.Instance.ReleaseAllUIInstances(1);
            _pauseUI = UIManager.Instance.ShowUIOnTop<PauseUI>(C.Layer.Pause, 1);
            _pauseUI.Setup(false, true);
            _pauseUI.OnHome += onPauseHome;
            _pauseUI.OnRestart += onPauseRestart;
            _pauseUI.OnResume += onPauseResume;
            _pauseUI.OnToggleSound += onPauseUpdateSound;
            _pauseUI.OnToggleBGM += onPauseUpdateBGM;
        }

        #region PAUSE
        private void onPauseHome()
        {
            OpenGiveUpPopUp(1);
        }

        private void onPauseRestart()
        {
            OpenGiveUpPopUp(2);
        }

        private void onPauseResume()
        {
            UIManager.Instance.ReleaseUI(_pauseUI, true);
        }

        private void onPauseUpdateSound(bool state)
        {
            if (state)
            {
                UnityEngine.Debug.Log("Sound is ON");
            }
            else
            {
                UnityEngine.Debug.Log("Sound is OFF");
            }
        }

        private void onPauseUpdateBGM(bool state)
        {
            if (state)
            {
                UnityEngine.Debug.Log("BGM is ON");
            }
            else
            {
                UnityEngine.Debug.Log("BGM is OFF");
            }
        }
        #endregion PAUSE

        


        #region Popup Button Actions

        private void onNextLevel()
        {
            loadLevel(LevelId + 1);
        }

        private void onClose()
        {
            UIManager.Instance.ReleaseAllUIInstances(0);
            UIManager.Instance.ReleaseAllUIInstances(1);
            Managers.AppManager.Instance.Switch(new AppStateMap() { });
        }

        private void onRetryLevel()
        {
            AppManager.Instance.StartCoroutine(restartLevel(LevelId));
        }

        private IEnumerator restartLevel(int level)
        {
            yield return Yielders.EndOfFrame;
            UIManager.Instance.ReleaseAllUIInstances(1);
            hideUIActionPhase();
            yield return Yielders.Get(0.5f);
            loadLevel(level);
        }

        private void onContinueLevel()
        {
            UIManager.Instance.ReleaseAllUIInstances(1);
            showUIActionPhase();
        }

        private void loadLevel(int level)
        {
            LevelId = level;
            UIManager.Instance.ReleaseAllUIInstances(0);
            UIManager.Instance.ReleaseAllUIInstances(1);
            showUIActionPhase();
        }

     
        private void onFtueStart()
        {
            UnityEngine.Debug.Log("clicked");
            UIManager.Instance.ReleaseAllUIInstances(0);
            UIManager.Instance.ReleaseAllUIInstances(1);
        }

        #endregion Popup Button Actions

        #region Open Popups

        private void OpenGiveUpPopUp(int type)
        {
            UIManager.Instance.ReleaseAllUIInstances(1);
            _levelGiveUpUI = UIManager.Instance.ShowUIOnTop<UIControllers.LevelGiveUpUI>(C.Layer.LevelGiveUp, 1);

            string title = "";
            switch (type)
            {
                case 0: // from Out Of Move
                    if (true)
                    {
                        title = "QUIT?";
                        _levelGiveUpUI.OnPlay += onContinueLevel;
                        _levelGiveUpUI.OnWatchAds += onContinueLevel;
                    }
                    break;
                case 1: // from Pause Home
                    title = "HOME?";
                    _levelGiveUpUI.OnPlay += onClose;
                    _levelGiveUpUI.OnPopupClose += onPauseClicked;
                    break;
                case 2: // from Pause Restart
                    title = "RESTART?";
                    _levelGiveUpUI.OnPlay += onRetryLevel;
                    _levelGiveUpUI.OnPopupClose += onPauseClicked;
                    break;
                default: break;
            }
            _levelGiveUpUI.Setup(title , type);
        }

        

        #endregion Open Popups
        
    }
}