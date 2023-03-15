using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace UI
{
    public class XProgressBar : MonoBehaviour
    {
        [SerializeField] Image _img;
        [SerializeField] float _duration = 2.0f;

        private Tweener _tweener;

        public float SetPercent(float percent, bool isAnimation = false, bool isLoop = false)
        {
            float t = 0;
            if (isAnimation)
            {
                if (_tweener != null)
                {
                    _img.DOKill();
                    _tweener = null;
                }
                if (!isLoop)
                {
                    t = (percent - _img.fillAmount) * _duration;
                    _tweener = _img.DOFillAmount(percent, t);
                }
                else//loop
                {
                    t = (1.0f - _img.fillAmount) * _duration;
                    _tweener = _img.DOFillAmount(1.0f, t).OnComplete(() =>
                    {
                        _img.fillAmount = 0;
                        _img.DOFillAmount(percent, percent * _duration * 0.7f).SetDelay(0.02f);
                    });
                }
            }
            else
            {
                _img.fillAmount = percent;
            }

            return t;
        }
    }
}
