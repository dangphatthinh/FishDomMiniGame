using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Athena.MiniGame.Fishdom2.Managers;
using System;
using Athena.MiniGame.Fishdom2.ZTask;
using Athena.MiniGame.Fishdom2.UIController;

namespace Athena.MiniGame.Fishdom2.AppFlow
{
    public class AppStateInitial : IState
    {
        public bool IsInitialized => _isInitialized;
        private bool _isInitialized = false;

        public void Initialize()
        {
            _isInitialized = true;
            TaskManager.StartCoroutine(corLoadGameData());
        }

        private IEnumerator corLoadGameData()
        {
            yield return G.Init();
            AppManager.Instance.Switch(new AppStateGamePlay());        
        }

        public void Resume() { }

        public void Clear() { }

        public void Exit() { }
    }
}

