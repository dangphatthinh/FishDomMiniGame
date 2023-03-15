using UnityEngine;
using Athena.Common.UI;
using System.Collections.Generic;
using UI;
using System;

namespace UIControllers
{
    public class CheatUI : UIController
    {
        public event System.Action<int> OnSelectLevel;
        public event System.Action OnWinClick;
        public event System.Action OnLoseClick;
        public event System.Action OnBackClick;

        public UI.XButton WinBtn;
        public UI.XButton LoseBtn;
        public UI.XButton BackBtn;

        [SerializeField] Transform _parent;
        [SerializeField] GameObject _levelPrefab;

        private List<GameObject> _levelObjects = new List<GameObject>();

        protected override void OnUIStart()
        {
            var levels = 30;
            for (int i = 0, c = levels; i < c; ++i)
            {
                var t = i;
                var levelBtn = Instantiate(_levelPrefab);
                levelBtn.transform.SetParent(_parent, false);
                levelBtn.SetActive(true);
                var btnScript = levelBtn.GetComponent<UI.XButton>();
                btnScript.Text = string.Format("level {0}", t + 1);
                btnScript.OnClicked += _ => selectLevel(t);
                _levelObjects.Add(levelBtn);
            }

            WinBtn.OnClicked += onWinClicked;
            LoseBtn.OnClicked += onLoseClicked;
            BackBtn.OnClicked += onBackClicked;
        }

        protected override void OnBack()
        {
            Debug.Log("OnBack");
        }

        protected override void OnUIRemoved()
        {
            foreach (var level in _levelObjects)
            {
                Destroy(level);
            }
            _levelObjects.Clear();

            WinBtn.RemoveAllListeners();
            LoseBtn.RemoveAllListeners();
            BackBtn.RemoveAllListeners();
        }

        private void selectLevel(int level)
        {
            if (OnSelectLevel != null) OnSelectLevel(level + 1);
        }

        private void onLoseClicked(XButton obj)
        {
            if (OnLoseClick != null) OnLoseClick();
        }

        private void onWinClicked(XButton obj)
        {
            if (OnWinClick != null) OnWinClick();
        }

        private void onBackClicked(XButton obj)
        {
            if (OnBackClick != null) OnBackClick();
        }
    }
}