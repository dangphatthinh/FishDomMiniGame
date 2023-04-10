using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Athena.MiniGame.Fishdom2.UIController;
using Athena.MiniGame.Fishdom2.AppFlow;

namespace Athena.MiniGame.Fishdom2.Managers
{
    public class AppManager : MonoBehaviour
    {
        public static AppManager Instance;
        private AppStateMachine _stateMachine;
        private LoadingUI _loadingUI;
        [SerializeField] private Transform _parent;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Debug.LogError("[SingletonMono<" + typeof(AppManager).Name + ">] Creating new Instance but " + Instance.gameObject.name + " existed, destroying: " + gameObject.name);
                Destroy(gameObject);
            }
            OnAwake();
        }
        private void OnAwake()
        {
            Application.targetFrameRate = 60;
            Input.multiTouchEnabled = false;

            _stateMachine = new AppStateMachine();

            SetupAthenaApp();
        }
        void SetupAthenaApp()
        {
            ShowLoadingUI();
            Switch(new AppStateInitial());
        }
        public void Switch(IState newState)
        {
            StartCoroutine(_stateMachine.SwitchProcess(newState));
        }
        public void ShowLoadingUI()
        {
            GameObject loadAsset = Resources.Load<GameObject>("UIPrefabs/LoadingUI");
            GameObject instance = Instantiate(loadAsset);
            instance.transform.SetParent(_parent);
            instance.GetComponent<RectTransform>().localScale = Vector3.one;
            _loadingUI = instance.GetComponent<LoadingUI>();
            _loadingUI.SetUp();
        }
    }
}
