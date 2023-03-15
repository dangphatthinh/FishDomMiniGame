using System;
using System.Collections;
using Athena.Common;
using Athena.Common.UI;
using CustomUtils;
using UIControllers;
using UnityEngine;

namespace Managers
{
    public class AppManager : SingletonMono<AppManager>
    {
        private AppStateMachine _stateMachine;

        private LoadingUI _loadingUI;

        public AppManager()
        {

        }

        protected override void OnAwake()
        {
            base.OnAwake();

            //Initialize app
            Application.targetFrameRate = 60;
            Input.multiTouchEnabled = false;

            _stateMachine = new AppStateMachine();

            //Setup athena app
            SetupAthenaApp();

        }

        public void Switch(IState newState)
        {
            StartCoroutine(_stateMachine.SwitchProcess(newState));
        }

        void SetupAthenaApp()
        {
            Switch(new AppFlow.AppStateInitial());
        }

        public void ShowLoadingUI()
        {
            _loadingUI = UIManager.Instance.ShowUIOnTop<LoadingUI>(C.Layer.Loading);
            _loadingUI.Setup();
        }

        public void HideLoadingUI()
        {
            UIManager.Instance.ReleaseUI(_loadingUI, true);
        }

        public void ShowWaitingUI()
        {
            // if (_waitingUI != null)
            // {
            //     UIManager.Instance.ShowUIOnTop(_waitingUI);
            //     _waitingUI.Setup();
            //     return;
            // }
            // _waitingUI = UIManager.Instance.ShowUIOnTop<WaitingUI>("WaitingUI");
        }

        public void HideWaitingUI()
        {
            // if (_waitingUI == null)
            //     return;

            // _waitingUI.Close();
        }
    }
}
