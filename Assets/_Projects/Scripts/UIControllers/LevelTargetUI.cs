using UnityEngine;
using Athena.Common.UI;
using System;
using UI;

namespace UIControllers
{
    public class LevelTargetUI : UIController
    {
        public event Action<int> OnStartGame;
        public event Action OnPopupClose;

        public UI.XButton StartBtn;
        public UI.XButton CloseBtn;
        public UI.XTextMesh TitleText;
        public UI.XTextMesh TargetText;

        public void Setup(LevelStation.Data levelData)
        {
            TitleText.Value = string.Format("Level {0}", levelData.LevelId);
            switch (levelData.LevelGoal)
            {
                case 0: TargetText.Value = "Pop all bubbles!"; break;
                case 1: TargetText.Value = "Collect all gems!"; break;
                default: break;
            }
            StartBtn.OnClicked += _ => StartGame(levelData.LevelId);
        }

        protected override void OnUIStart()
        {
            CloseBtn.OnClicked += _ => ClosePopup();
        }

        protected override void OnBack()
        {
            Debug.Log("OnBack");
        }

        protected override void OnUIRemoved()
        {
            StartBtn.RemoveAllListeners();
            CloseBtn.RemoveAllListeners();
        }

        private void StartGame(int levelId)
        {
            OnStartGame?.Invoke(levelId);
        }

        private void ClosePopup()
        {
            OnPopupClose?.Invoke();
        }
    }
}