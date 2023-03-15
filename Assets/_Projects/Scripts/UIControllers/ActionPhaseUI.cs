using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Athena.Common.UI;
using TMPro;
using DG.Tweening;
using System;
using UnityEngine.SocialPlatforms.Impl;

namespace UIControllers
{
    public class ActionPhaseUI : UIController
    {
        public event Action<int> OnActiveBooster;
        public event Action OnCancelBooster;
        public event Action OnSwapBubble;
        public event Action OnPause;

        public UI.XNumberLabel ScoreLabel;
        public UI.LevelTarget LevelTarget;
        public UI.XNumberLabel BulletRemainLabel;

        public UI.XButton SwapBubbleBtn;
        public UI.XButton CancelBoosterBtn;
        public UI.XButton PauseBtn;

        public void SetUp()
        {
            ScoreLabel.Init(0);
        }

        public void OnActiceBooster()
        {
        }

        public void OnDeactiveBooster()
        {
        }

        protected override void OnUIStart()
        {
            PauseBtn.OnClicked += _ => pauseClicked();
        }

        protected override void OnBack()
        {
            Debug.Log("OnBack");
        }

        protected override void OnUIRemoved()
        {
            CancelBoosterBtn.RemoveAllListeners();
        }

        private void pauseClicked()
        {
            if (OnPause != null) OnPause();
        }

    }
}