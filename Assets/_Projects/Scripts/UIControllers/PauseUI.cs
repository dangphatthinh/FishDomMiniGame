using UnityEngine;
using Athena.Common.UI;
using System;

namespace UIControllers
{
    public class PauseUI : UIController
    {
        public event Action OnHome;
        public event Action OnRestart;
        public event Action OnResume;
        public event Action OnDebug;
        public event Action<bool> OnToggleSound;
        public event Action<bool> OnToggleBGM;

        public UI.XButton homeBtn;
        public UI.XButton restartBtn;
        public UI.XButton resumeBtn;
        public UI.XButton debugBtn;
        public UI.XToggle soundToggle;
        public UI.XToggle bgmToggle;
        
        public void Setup(bool sound, bool bgm)
        {
        }
        protected override void OnUIStart()
        {
            homeBtn.OnClicked += _ => Home();
            restartBtn.OnClicked += _ => Restart();
            resumeBtn.OnClicked += _ => Resume();
            debugBtn.OnClicked += _ => Debug();
        }

        protected override void OnBack()
        {

        }

        protected override void OnUIRemoved()
        {
            homeBtn.RemoveAllListeners();
            restartBtn.RemoveAllListeners();
            resumeBtn.RemoveAllListeners();
        }

        private void Home()
        {
            OnHome?.Invoke();
        }

        private void Restart()
        {
            OnRestart?.Invoke();
        }

        private void Resume()
        {
            OnResume?.Invoke();
        }

        private void Debug()
        {
            OnDebug?.Invoke();
        }

        private void ToggleSound()
        {
            OnToggleSound?.Invoke(soundToggle.State);
        }

        private void ToggleBGM()
        {
            OnToggleBGM?.Invoke(bgmToggle.State);
        }
    }
}