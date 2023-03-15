using UnityEngine;
using Athena.Common.UI;
using System;

namespace UIControllers
{
    public class LevelOutOfMoveUI : UIController
    {
        public event Action OnPlay;
        public event Action OnWatchAds;
        public event Action<int> OnPopupClose;

        public UI.XButton playBtn;
        public UI.XButton watchAdsBtn;
        public UI.XButton closeBtn;

        public void Setup()
        {
            
        }

        protected override void OnUIStart()
        {
            playBtn.OnClicked += _ => Play();
            watchAdsBtn.OnClicked += _ => WatchAds();
            closeBtn.OnClicked += _ => Close();
        }

        protected override void OnBack()
        {
            Debug.Log("OnBack");
        }

        protected override void OnUIRemoved()
        {
            playBtn.RemoveAllListeners();
            watchAdsBtn.RemoveAllListeners();
            closeBtn.RemoveAllListeners();
        }

        private void Play()
        {
            OnPlay?.Invoke();
        }

        private void WatchAds()
        {
            OnWatchAds?.Invoke();
        }

        private void Close()
        {
            OnPopupClose?.Invoke(0);
        }
    }
}