using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Athena.Common.UI;
using TMPro;
using DG.Tweening;

namespace UIControllers
{
    public class LoadingUI : UIController
    {
        [SerializeField]
        private TextMeshProUGUI _loadingText;
        [SerializeField]
        private Slider _progressBar;

        private float _progress = 0f;

        public void Setup()
        {
            _progressBar.value = 0f;
            SetText("1%");
            StartCoroutine(LoadingProcess());
        }

        private IEnumerator LoadingProcess()
        {
            while (gameObject.activeSelf)
            {
                _progressBar.value += 0.02f;
                SetText(Mathf.FloorToInt(_progressBar.value * 100).ToString());
                yield return null;
            }

            // _progressBar.value = _progress;
        }

        public void SetText(string loadingText)
        {
            _loadingText.text = loadingText;
        }

        public void SetProgress(float progress)
        {
            _progress += progress;
        }

        public bool IsLoading()
        {
            return (_progressBar.value < 1f);
        }
    }
}