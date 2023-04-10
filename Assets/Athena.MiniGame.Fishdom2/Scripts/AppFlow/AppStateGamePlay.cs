using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Athena.MiniGame.Fishdom2.Managers;
using UnityEngine.SceneManagement;
using Athena.MiniGame.Fishdom2.GamePlay;

namespace Athena.MiniGame.Fishdom2.AppFlow
{
    public class AppStateGamePlay : IState
    {
        private bool _isInitialized = false;
        public bool IsInitialized => _isInitialized;

        private GameController _gameController;

        public void Initialize()
        {
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
        }

        public void Exit() { }
        public void Resume() { }
        public void Clear() { }
    }

}
