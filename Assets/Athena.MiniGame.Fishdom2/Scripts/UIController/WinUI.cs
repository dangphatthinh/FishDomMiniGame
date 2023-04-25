using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Athena.MiniGame.Fishdom2.Data;
using UI;



namespace Athena.MiniGame.Fishdom2.UIController
{
    public class WinUI : Athena.Common.UI.UIController
    {
        public event Action OnNewLevel;

        public XButton newBtn;
        public XNumberLabel scoreText;
        public Animator stars;

        public void SetUp(GameData data)
        {
            stars.Play(string.Format("Base Layer.WinningStar_{0}", 3));
            scoreText.Set(data.Score, true);
        }
        protected override void OnUIStart()
        {
            newBtn.OnClicked += _ => restart();
        }
        private void restart()
        {
            if (OnNewLevel != null) OnNewLevel();
        }
        protected override void OnUIRemoved()
        {
            newBtn.RemoveAllListeners();
        }
    }
}
