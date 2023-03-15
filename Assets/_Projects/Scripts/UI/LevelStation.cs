using UnityEngine;
using System;

namespace UI
{
    public class LevelStation : MonoBehaviour
    {
        public event Action<LevelStation.Data> OnClickEvent;

        [SerializeField] GameObject _background;
        [SerializeField] GameObject _stars;
        [SerializeField] UI.XTextMesh _titleText;
        [SerializeField] UI.XButton _self;

        public void Setup(LevelStation.Data data)
        {
            _titleText.Value = string.Format("{0}", data.LevelId);

            _self.RemoveAllListeners();
            _self.OnClicked += _ => OnClick(data);
        }

        private void OnClick(LevelStation.Data data)
        {
            OnClickEvent?.Invoke(data);
        }

        public class Data
        {
            public int LevelId;
            public bool IsHard; // is hard level
            public bool IsChest; // the level has chest
            public int LevelGoal; // 0: pop bubbles, 1: collect gems
            public int Star; // highest number of stars achieved of the level
            public bool IsUnlocked; // is level unlocked (able to play level)

            public Data(int levelId, bool isHard, bool isChest, int levelGoal, int star, bool isUnlocked)
            {
                LevelId = levelId;
                IsHard = isHard;
                IsChest = isChest;
                LevelGoal = levelGoal;
                Star = star;
                IsUnlocked = isUnlocked;
            }
        }
    }
}
