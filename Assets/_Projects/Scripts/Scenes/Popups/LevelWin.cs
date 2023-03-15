using System;
using UnityEngine;

namespace Scenes.Popups
{
    public class LevelWin : MonoBehaviour, IPopup
    {
        public event Action OnNextLevel;

        public UI.XButton RetryBtn;

        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            hide();
        }

        public void OnEnable()
        {
            RetryBtn.OnClicked += _ => playNextLevel();
        }

        public void OnDisable()
        {
            RetryBtn.RemoveAllListeners();
        }

        private void playNextLevel()
        {
            if (OnNextLevel != null) OnNextLevel();
        }

        private void hide()
        {
            gameObject.SetActive(false);
        }
    }
}