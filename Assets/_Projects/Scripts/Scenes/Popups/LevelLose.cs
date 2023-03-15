using System;
using UI;
using UnityEngine;

namespace Scenes.Popups
{
    public class LevelLose : MonoBehaviour, IPopup
    {
        public event Action OnRetry;

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
            RetryBtn.OnClicked += _ => retry();
        }

        public void OnDisable()
        {
            RetryBtn.RemoveAllListeners();
        }

        private void retry()
        {
            if (OnRetry != null) OnRetry();
        }

        private void hide()
        {
            gameObject.SetActive(false);
        }
    }
}