using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Tool
{
    public class UIController : MonoBehaviour
    {
        public static UIController Instance;
        [SerializeField] private GameObject _settingUI;
        public int CurrentIndex = 0;

        private void Start()
        {
            if(Instance == null)
            {
                Instance = this;
            }
        }

        public void OpenSettingUI()
        {
            _settingUI.SetActive(true);
        }
        public void CloseSettingUI()
        {
            _settingUI.SetActive(false);
        }
    }
}

