using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Athena.Common.UI;

namespace Athena.MiniGame.Fishdom2.UIController
{
    public class LoadingUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _loadingText;
        [SerializeField] private Slider _progressBar;

        public void SetUp()
        {
            _progressBar.value = 0f;
            SetText("1%");
            StartCoroutine(LoadingProgress());
        }

        private IEnumerator LoadingProgress()
        {
            while(IsLoading())
            {
                SetProgress(0.02f);
                SetText("Loading " + Mathf.FloorToInt(_progressBar.value * 100).ToString() + "%");
                yield return null;
            }
        }
        public void SetText(string loadingText)
        {
            _loadingText.text = loadingText;
        }
        public void SetProgress(float progress)
        {
            _progressBar.value += progress;
        }
        public bool IsLoading()
        {
            return (_progressBar.value < 1f);
        }
    }
}

