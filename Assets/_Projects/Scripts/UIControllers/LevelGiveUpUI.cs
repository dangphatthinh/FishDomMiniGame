using UnityEngine;
using Athena.Common.UI;
using System;

namespace UIControllers
{
    public class LevelGiveUpUI : UIController
    {
        public event Action OnPlay;
        public event Action OnWatchAds;
        public event Action OnPopupClose;

        public UI.XTextMesh titleText;
        public UI.XButton playBtn;
        public UI.XButton watchAdsBtn;
        public UI.XButton yesBtn;
        public UI.XButton noBtn;
        public UI.XButton closeBtn;
        public UI.XTextMesh lostAmount;
        public GameObject[] lostResources;

        public void Setup(string title, int type)
        {
            
            if (type == 0) //Out of moves
            {
                yesBtn.gameObject.SetActive(false);
                noBtn.gameObject.SetActive(false);
            } else //pause menu
            {
                yesBtn.gameObject.SetActive(true);
                noBtn.gameObject.SetActive(true);
            }
        }

        protected override void OnUIStart()
        {
            //playBtn.OnClicked += _ => Play();
            //watchAdsBtn.OnClicked += _ => WatchAds();
            closeBtn.OnClicked += _ => Close();

            noBtn.OnClicked += _ => Close();
            yesBtn.OnClicked += _ => Play();
        }

        protected override void OnBack()
        {
            Debug.Log("OnBack");
        }

        protected override void OnUIRemoved()
        {
            closeBtn.RemoveAllListeners();
            noBtn.RemoveAllListeners();
            yesBtn.RemoveAllListeners();
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
            OnPopupClose?.Invoke();
        }
    }
}