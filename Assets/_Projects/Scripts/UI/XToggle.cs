using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI
{
    public class XToggle : MonoBehaviour, IPointerDownHandler, IPointerClickHandler
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
        [SerializeField]
        private GameObject _positiveState = null;
        [SerializeField]
        private GameObject _negativeState = null;

        private List<Action<XToggle>> _delegates = new List<Action<XToggle>>();

        private Action<XToggle> _realOnClicked;
        private bool _isEnable = true;
        private bool _isSoundEnable = true;
        private string _text = "";
        private bool _state;

        public event Action<XToggle> OnDisableClicked;

        public event Action<XToggle> OnClicked
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

        public bool State
        {
            get { return _state; }
            set
            {
                UpdateState(value);
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

                if (_realOnClicked != null)
                {
                    UpdateState(!_state);
                    _realOnClicked(this);
                }
                _lastTimeTouch = Time.timeSinceLevelLoad;
                var audioId = string.IsNullOrEmpty(_optAudioId) ? C.AudioIds.Sound.TouchBubble : _optAudioId;
            }
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (!_isEnable) return;
        }

        private void UpdateState(bool newState)
        {
            _state = newState;
            if (_positiveState != null && _negativeState != null)
            {
                _positiveState.SetActive(_state);
                _negativeState.SetActive(!_state);
            }       
        }
    }
}
