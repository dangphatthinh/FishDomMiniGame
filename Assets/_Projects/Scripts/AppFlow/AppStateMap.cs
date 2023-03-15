using System;
using Athena.Common;
using Athena.Common.UI;
using UI;
using Managers;
using Scenes.Popups;
using UIControllers;
using UnityEngine;

namespace AppFlow
{
    public class AppStateMap : IState
    {
        public bool IsInitialized => _isInitialized;

        private bool _isInitialized = false;

        private UIControllers.HomeUI _homeUI;
        private UIControllers.LevelTargetUI _levelTargetUI;
        private UIControllers.SettingUI _settingUI;
        private UIControllers.ShopUI _shopUI;

        public void Initialize()
        {
            //_levelSelectUI = UIManager.Instance.ShowUIOnTop<UIControllers.HomeUI>(C.Layer.LevelSelect);
            _homeUI = UIManager.Instance.ShowUIOnTop<UIControllers.HomeUI>(C.Layer.Home);
            _homeUI.OnSelectLevel += onSelectLevel;
            _homeUI.OnSetting += onSettingBtn;
            _homeUI.OnShop += onShopBtn;
            _homeUI.OnPlayNow += onPlayNow;
            AppManager.Instance.HideLoadingUI();
            _isInitialized = true;
        }

        public void Resume()
        {
        }

        public void Clear()
        {
            UIManager.Instance.ReleaseUI(_homeUI, true);
        }

        public void Exit()
        {

        }

        private void onSelectLevel()
        {
            _levelTargetUI = UIManager.Instance.ShowUIOnTop<UIControllers.LevelTargetUI>(C.Layer.LevelTarget, 1);
            _levelTargetUI.OnStartGame += onLevelTargetStartBtn;
            _levelTargetUI.OnPopupClose += onLevelTargetCloseBtn;
        }

        private void onPlayNow()
        { 
            AppManager.Instance.Switch(new AppStateGameplay());
        }

        private void onLevelTargetStartBtn(int level)
        {
            onLevelTargetCloseBtn();
            AppManager.Instance.Switch(new AppStateGameplay() { LevelId = level });
        }

        private void onLevelTargetCloseBtn()
        {
        //    _levelTargetUI.OnStartGame -= onLevelTargetStartBtn;
        //    UIManager.Instance.ReleaseUI(_levelTargetUI, true);
        }

        #region SHOP
        private void onShopBtn()
        {
            _shopUI = UIManager.Instance.ShowUIOnTop<UIControllers.ShopUI>(C.Layer.Shop, 1);
            _shopUI.OnPopupClose += onShopCloseBtn;
        }

        private void onShopCloseBtn()
        {
            UIManager.Instance.ReleaseUI(_shopUI, true);
        }

        #endregion SHOP

        #region SETTING

        private void onSettingBtn()
        {
            _settingUI = UIManager.Instance.ShowUIOnTop<UIControllers.SettingUI>(C.Layer.Setting, 1);
            _settingUI.Setup("0.0.1", "abcdef12_ghi_k3lm", false, true);
            _settingUI.OnContactUs += onSettingContactUs;
            _settingUI.OnDebug += onSettingDebug;
            _settingUI.OnPopupClose += onSettingCloseBtn;
            _settingUI.OnToggleSound += onSettingUpdateSound;
            _settingUI.OnToggleBGM += onSettingUpdateBGM;
        }

        private void onSettingContactUs()
        {
            Debug.Log("Contact Us is clicked");
        }

        private void onSettingDebug()
        {
            Debug.Log("Debug is clicked");
        }

        private void onSettingCloseBtn()
        {
            UIManager.Instance.ReleaseUI(_settingUI, true);
        }

        private void onSettingUpdateSound(bool state)
        {
            if (state)
            {
                Debug.Log("Sound is ON");
            }
            else
            {
                Debug.Log("Sound is OFF");
            }
        }

        private void onSettingUpdateBGM(bool state)
        {
            if (state)
            {
                Debug.Log("BGM is ON");
            }
            else
            {
                Debug.Log("BGM is OFF");
            }
        }

        #endregion SETTING
    }
}