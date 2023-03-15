using System;
using System.Collections;
using Athena.Common;
using Managers;
using UnityEngine;

namespace AppFlow
{
    public class AppStateInitial : IState
    {
        public bool IsInitialized => _isInitialized;
        private bool _isInitialized = false;

        public void Initialize()
        {
            _isInitialized = true;
            AppManager.Instance.Switch(new AppStateMap());
        }

        private IEnumerator corLoadGameData()
        {
            yield return 0;
            //AppManager.Instance.Switch(new AppStateMap());
        }

        public void Resume() { }

        public void Clear() { }

        public void Exit() { }
    }
}