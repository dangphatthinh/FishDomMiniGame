using UnityEngine;
using Athena.Common.UI;
using System;

namespace UIControllers
{
    public class SettingUI : UIController
    {
        public event Action OnContactUs;
        public event Action OnDebug;
        public event Action OnPopupClose;
        public event Action<bool> OnToggleSound;
        public event Action<bool> OnToggleBGM;

        public UI.XTextMesh versionText;
        public UI.XTextMesh userIDText;
        public UI.XButton contactUsBtn;
        public UI.XButton debugBtn;
        public UI.XButton closeBtn;
        public UI.XToggle soundToggle;
        public UI.XToggle bgmToggle;



        public void Setup(string version, string userID, bool sound, bool bgm)
        {
            versionText.Value = string.Format("Version {0}", version);
            userIDText.Value = string.Format("User ID: {0}", userID);
            soundToggle.State = sound;
            bgmToggle.State = bgm;
        }
        protected override void OnUIStart()
        {
            contactUsBtn.OnClicked += _ => Contact();
            debugBtn.OnClicked += _ => Debug();
            closeBtn.OnClicked += _ => Close();
            soundToggle.OnClicked += _ => ToggleSound();
            bgmToggle.OnClicked += _ => ToggleBGM();
        }

        protected override void OnBack()
        {
            
        }

        protected override void OnUIRemoved()
        {
            contactUsBtn.RemoveAllListeners();
            debugBtn.RemoveAllListeners();
            closeBtn.RemoveAllListeners();
            soundToggle.RemoveAllListeners();
            bgmToggle.RemoveAllListeners();
        }

        private void Contact()
        {
            OnContactUs?.Invoke();
        }

        private void Debug()
        {
            OnDebug?.Invoke();
        }

        private void Close()
        {
            OnPopupClose?.Invoke();
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