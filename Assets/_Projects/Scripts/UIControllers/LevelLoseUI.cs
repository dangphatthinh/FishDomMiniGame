using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Athena.Common.UI;
using TMPro;
using DG.Tweening;
using System;
using Scenes.Popups;

namespace UIControllers
{
    public class LevelLoseUI : UIController
    {
        public event Action OnRetry;
        public event Action OnPopupClose;

        public UI.XButton retryBtn;
        public UI.XButton closeBtn;
        public UI.XTextMesh titleText;
        public UI.XTextMesh failMessage;

        public void Setup(int gameObjectiveType)
        {
            switch (gameObjectiveType)
            {
                case 0: failMessage.Value = "Pop all bubbles!"; break;
                case 1: failMessage.Value = "Collect all gems!"; break;
                default: break;
            }
        }

        //public void Setup(LevelResultData data, int gameObjectiveType)
        //{
        //    titleText.Value = string.Format("Level {0}", data.LevelId);
        //    switch (gameObjectiveType)
        //    {
        //        case 0: failMessage.Value = "Pop all bubbles!"; break;
        //        case 1: failMessage.Value = "Collect all gems!"; break;
        //        default: break;
        //    }
        //}

        protected override void OnUIStart()
        {
            retryBtn.OnClicked += _ => Retry();
            closeBtn.OnClicked += _ => Close();
        }

        protected override void OnBack()
        {
            Debug.Log("OnBack");
        }

        protected override void OnUIRemoved()
        {
            retryBtn.RemoveAllListeners();
            closeBtn.RemoveAllListeners();
        }

        private void Retry()
        {
            OnRetry?.Invoke();
        }

        private void Close()
        {
            OnPopupClose?.Invoke();
        }
    }
}