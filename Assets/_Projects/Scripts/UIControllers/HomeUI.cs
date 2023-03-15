using UnityEngine;
using Athena.Common.UI;
using System.Collections.Generic;
using UI;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;
using System.Drawing;

namespace UIControllers
{
    public class HomeUI : UIController
    {
        [Header("Headers")]
        [SerializeField] RectTransform _topUI;
        [SerializeField] RectTransform _botUI;
        [SerializeField] RectTransform _leftUI;
        [SerializeField] RectTransform _rightUI;

        [Header("Special Buttons")]
        [SerializeField] UI.XButton _settingBtn;
        [SerializeField] UI.XButton _shopBtn;
        [SerializeField] UI.XButton _playNowBtn;

        [Header("Map")]
        [SerializeField] Transform _parent;
        [SerializeField] MapDrag _mapDrag;
        [SerializeField] GameObject _levelStationPrefab;
        [SerializeField] RectTransform[] _levelStationPositions;

        public event System.Action OnSelectLevel;
        public event System.Action OnSetting;
        public event System.Action OnShop;
        public event System.Action OnPlayNow;

        private List<GameObject> _levelObjects = new List<GameObject>();
        private bool _isHeaderUIShown = true;

        protected override void OnUIStart()
        {
            _isHeaderUIShown = true;
            //var levels = G.DataService.Levels;
            var levels = 4;

            //for (int i = 0, c = levels; i < c; ++i)
            //{
            //    var levelStation = Instantiate(_levelStationPrefab);
            //    levelStation.transform.SetParent(_levelStationPositions[i], false);
            //    levelStation.SetActive(true);

            //    var btnScript = levelStation.GetComponent<UI.LevelStation>();

            //    LevelStation.Data data = new LevelStation.Data(i + 1, false, false, 0, 3, true);
            //    btnScript.OnClickEvent += _ => SelectLevel();
            //    btnScript.Setup(data);
            //    _levelObjects.Add(levelStation);
            //}
            _playNowBtn.Text = string.Format("LEVEL {0}", 1);
            _settingBtn.OnClicked += _ => OnSettingBtnClick();
            _shopBtn.OnClicked += _ => OnShopBtnClick();
            _playNowBtn.OnClicked += _ => OnPlayNowBtnClick();

            _mapDrag.OnDragEvent += OnMapDrag;
            _mapDrag.OnBeginDragEvent += HideUIHeader;
            _mapDrag.OnEndDragEvent += ShowUIHeader;
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
            _settingBtn.RemoveAllListeners();
            _shopBtn.RemoveAllListeners();
            _playNowBtn.RemoveAllListeners();

            _mapDrag.OnDragEvent -= OnMapDrag;
            _mapDrag.OnBeginDragEvent -= ShowUIHeader;
            _mapDrag.OnEndDragEvent -= HideUIHeader;
        }

        private void SelectLevel()
        {
            OnSelectLevel?.Invoke();
        }

        private void OnSettingBtnClick()
        {
            OnSetting?.Invoke();
        }

        private void OnShopBtnClick()
        {
            OnShop?.Invoke();
        }

        private void OnPlayNowBtnClick()
        {
            OnPlayNow?.Invoke();
        }

        private void OnMapDrag()
        {
            //handle on map drag
        }

        private void HideUIHeader()
        {
            if (_isHeaderUIShown)
            {
                _isHeaderUIShown = false;
                float height = _mapDrag.rectHeight / 2;
                float width = _mapDrag.rectWidth / 2;

                _topUI.DOLocalMoveY(height + 200, 1);
                _leftUI.DOLocalMoveX(-width - 300, 1);
                _rightUI.DOLocalMoveX(width + 300, 1);
            }
        }

        private void ShowUIHeader()
        {
            if (!_isHeaderUIShown)
            {
                _isHeaderUIShown = true;
                float height = _mapDrag.rectHeight / 2;
                float width = _mapDrag.rectWidth / 2;

                _topUI.DOLocalMoveY(height - 30, 1);
                _leftUI.DOLocalMoveX(-width + 25, 1);
                _rightUI.DOLocalMoveX(width - 25, 1);
            }
        }
    }
}