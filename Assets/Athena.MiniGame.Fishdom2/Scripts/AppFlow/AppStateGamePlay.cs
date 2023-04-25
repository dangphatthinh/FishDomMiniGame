using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Athena.MiniGame.Fishdom2.Managers;
using UnityEngine.SceneManagement;
using Athena.MiniGame.Fishdom2.GamePlay;
using Athena.MiniGame.Fishdom2.UIController;
using Athena.Common.UI;

namespace Athena.MiniGame.Fishdom2.AppFlow
{
    public class AppStateGamePlay : IState, IGameStateListener
    {
        private bool _isInitialized = false;
        public bool IsInitialized => _isInitialized;

        private GameController _gameController;

        private WinUI _winUI;
        private LoseUI _loseUI;

        public void Initialize()
        {
            AppManager.Instance.HideLoadingUI();
            SceneManager.sceneLoaded += onSceneLoaded;
            SceneManager.LoadScene("gameplay", LoadSceneMode.Single);
        }

        private void onSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            SceneManager.sceneLoaded -= onSceneLoaded;
            _isInitialized = true;
            var root = scene.GetRootGameObjects()[0];
            _gameController = root.GetComponent<GameController>();
            _gameController.Initialize();
            _gameController.SetGameStateListener(this);
        }

        IEnumerator OpenWinPopup()
        {
            yield return new WaitForSeconds(1.2f);
            _winUI = UIManager.Instance.ShowUIOnTop<WinUI>(C.Layer.WinUI, 1);
            _winUI.SetUp(_gameController.GetGameData());
            _winUI.OnNewLevel += onNewLevel;
        }
        IEnumerator OpenLosePopup()
        {
            yield return new WaitForSeconds(1.2f);
            _loseUI = UIManager.Instance.ShowUIOnTop<LoseUI>(C.Layer.LoseUI, 1);
            _loseUI.SetUp();
            _loseUI.OnRestartLevel += onRestartLevel;
        }
        private void onRestartLevel()
        {
            UIManager.Instance.ReleaseUI(_loseUI, true);
            Initialize();
        }
        private void onNewLevel()
        {
            UIManager.Instance.ReleaseUI(_winUI, true);
            Initialize();
        }

        public void OnGameComplete()
        {
            if(_gameController.IsWin())
            {
                ZTask.TaskManager.StartCoroutine(OpenWinPopup());
                return;
            }
            ZTask.TaskManager.StartCoroutine(OpenLosePopup());
        }

        public void Exit() { }
        public void Resume() { }
        public void Clear() { }
    }

}
