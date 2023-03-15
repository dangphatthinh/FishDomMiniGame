using UnityEngine;
using Athena.Common.UI;
using System;

namespace UIControllers
{
    public class LevelWinUI : UIController
    {
        public event Action OnNext;
        public event Action OnPopupClose;

        public GameObject highScoreNode;
        public UI.XButton nextBtn;
        public UI.XButton closeBtn;
        public UI.XTextMesh titleText;
        public UI.XNumberLabel scoreText;
        public Animator stars;
        
        public void Setup()
        {
            
        }
        protected override void OnUIStart()
        {
            nextBtn.OnClicked += _ => next();
            closeBtn.OnClicked += _ => close();
        }

        protected override void OnBack()
        {
            Debug.Log("OnBack");
        }

        protected override void OnUIRemoved()
        {
            nextBtn.RemoveAllListeners();
            closeBtn.RemoveAllListeners();
        }

        private void next()
        {
            if (OnNext != null) OnNext();
        }

        private void close()
        {
            if (OnPopupClose != null) OnPopupClose();
        }
    }
}