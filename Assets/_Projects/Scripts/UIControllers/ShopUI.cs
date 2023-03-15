using UnityEngine;
using Athena.Common.UI;
using System;

namespace UIControllers
{
    public class ShopUI : UIController
    {
        public event Action OnPopupClose;
        public event Action<string> OnOffferClick; 

        public UI.XButton closeBtn;

        public void Setup()
        {
            
        }
        protected override void OnUIStart()
        {
            closeBtn.OnClicked += _ => Close();
        }

        protected override void OnBack()
        {

        }

        protected override void OnUIRemoved()
        {
            closeBtn.RemoveAllListeners();
        }

        private void Close()
        {
            OnPopupClose?.Invoke();
        }

        private void OfferClick()
        {
            OnOffferClick?.Invoke("Item_Id");
        }
    }
}