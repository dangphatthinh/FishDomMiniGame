using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Athena.MiniGame.Fishdom2.UIController
{
        public class XButton : MonoBehaviour, IPointerDownHandler, IPointerClickHandler
        {
            const float DelayForAntiDoubleClick = 1.1f;

            [SerializeField]
            private Image _optImage = null;
            [SerializeField]
            private string _optAudioId = null;
            [SerializeField]
            private UnityEngine.UI.Button _optButton = null;
            [SerializeField]
            private TextMeshProUGUI _optText = null;

            private List<Action<XButton>> _delegates = new List<Action<XButton>>();

            private Action<XButton> _realOnClicked;
            private bool _isEnable = true;
            private bool _isSoundEnable = true;
            private string _text = "";

            public event Action<XButton> OnDisableClicked;

            public event Action<XButton> OnClicked
            {
                add
                {
                    _realOnClicked += value;
                    _delegates.Add(value);
                }

                remove
                {
                    _realOnClicked -= value;
                    _delegates.Remove(value);
                }
            }

            public Color Color
            {
                set
                {
                    if (_optImage != null)
                    {
                        _optImage.color = value;
                    }
                }
            }

            public bool Enable
            {
                get { return _isEnable; }
                set
                {
                    _isEnable = value;
                    if (_optButton != null)
                    {
                        _optButton.interactable = _isEnable;
                    }
                }
            }

            public string Text
            {
                get { return _text; }
                set
                {
                    _text = value;
                    if (_optText != null)
                    {
                        _optText.text = _text;
                    }
                }
            }

            public void RemoveAllListeners()
            {
                for (int i = 0, c = _delegates.Count; i < c; ++i)
                {
                    var d = _delegates[i];
                    _realOnClicked -= d;
                }
                _delegates.Clear();
            }

            private double _lastTimeTouch = 0;

            public void OnPointerClick(PointerEventData eventData)
            {
                if (!_isEnable)
                {
                    if (OnDisableClicked != null) OnDisableClicked(this);
                    return;
                }

                if (Time.timeSinceLevelLoad - _lastTimeTouch > DelayForAntiDoubleClick)
                {
                    if (_realOnClicked != null) _realOnClicked(this);
                    _lastTimeTouch = Time.timeSinceLevelLoad;
                }
            }

            public void OnPointerDown(PointerEventData eventData)
            {
                if (!_isEnable) return;
            }
        }
}

