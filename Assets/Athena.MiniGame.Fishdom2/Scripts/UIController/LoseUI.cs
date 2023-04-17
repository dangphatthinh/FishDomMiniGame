using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Athena.MiniGame.Fishdom2.UIController
{
    public class LoseUI : Athena.Common.UI.UIController
    {
        public event Action OnRestartLevel;

        public XButton restartBtn;

        public void SetUp()
        {
            
        }
        protected override void OnUIStart()
        {
            restartBtn.OnClicked += _ => restart();
        }
        private void restart()
        {
            if (OnRestartLevel != null) OnRestartLevel();
        }
        protected override void OnUIRemoved()
        {
            restartBtn.RemoveAllListeners();
        }
    }
}

