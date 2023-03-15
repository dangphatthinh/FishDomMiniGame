using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

namespace UI
{
    public class XTextMesh : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI _text;
        private string _value;

        public void Awake()
        {
            if (_text == null) throw new System.NullReferenceException();
        }

        public string Value
        {
            get { return _value; }
            set
            {
                _value = value;
                callSetter();
            }
        }

        public Color Color
        {
            set { _text.color = value; }
        }

        private void callSetter()
        {
            _text.text = _value;
        }
    }
}
