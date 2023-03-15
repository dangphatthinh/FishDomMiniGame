using UnityEngine;
using DG.Tweening;

namespace UI
{
    public class XNumberLabel : MonoBehaviour
    {
        public XTextMesh Label;
        public float MaxDurationPerAnim = 2.0f;

        private int _value;
        private int _finalValue;
        private float _timeElapsed;

        public void Init(int value)
        {
            _value = value;
            _finalValue = _value;
            _timeElapsed = 0;
            updatePresenters();
        }

        public void Set(int finalValue, bool useEffect)
        {
            if (useEffect)
            {
                finishCurrentAnimIfAny();
                _finalValue = finalValue;
                _timeElapsed = 0;
            }
            else
            {
                if (_value != finalValue)
                {
                    _value = finalValue;
                    _finalValue = _value;
                }
                updatePresenters();
            }
        }

        public void Update()
        {
            if (_value != _finalValue)
            {
                if (_timeElapsed > MaxDurationPerAnim)
                {
                    finishCurrentAnimIfAny();
                }
                else
                {
                    _timeElapsed += Time.deltaTime;
                    float tmp = Mathf.Lerp(_value, _finalValue, _timeElapsed / MaxDurationPerAnim);
                    int intValue = Mathf.CeilToInt(tmp);
                    if (intValue >= _value)
                    {
                        _value = intValue;
                        updatePresenters();
                    }
                }
            }
        }

        public void finishCurrentAnimIfAny()
        {
            if (_value != _finalValue)
            {
                _value = _finalValue;
                updatePresenters();
            }
        }

        private void updatePresenters()
        {
            Label.Value = _value.ToString();
        }
    }
}
