using Athena.Common.UI;
using UnityEngine;

namespace Helpers
{
    public class PopupViewAnimation : MonoBehaviour, IActiveUIListener
    {
        public UIController UIController;
        [SerializeField] Animator _animator;
        [SerializeField] string popupOpen = "PopUp_In";
        [SerializeField] string popupClose = "PopUp_Out";

        public void Awake()
        {
            if (UIController == null) throw new System.InvalidOperationException();
            UIController.RegisterActiveListener(this);
        }

        public void OnStartUI(UIController controller) { }

        public void OnActiveUI(UIController controller)
        {
            _animator.Play(popupOpen);
        }

        public void OnDeactiveUI(UIController controller)
        {
            _animator.Play(popupClose);
        }

        public void OnRemoveUI(UIController controller)
        {
            UIController.UnregisterActiveListener(this);
        }
    }
}