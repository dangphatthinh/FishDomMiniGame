using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Scenes
{
    public class ActionPhase : MonoBehaviour
    {
        public UI.XNumberLabel ScoreLabel;
        public UI.LevelTarget LevelTarget;
        public TextMeshPro BulletRemainLabel;

        public UI.XButton BombBoosterBtn;
        public UI.XButton RocketBoosterBtn;
        public UI.XButton LightningBoosterBtn;
        public UI.XButton RainbowBoosterBtn;
        public UI.XButton CancelBoosterBtn;


        public Popups.LevelWin LevelWinPopup;
        public Popups.LevelLose LevelLosePopup;


        private GameState _gameState;
        private GameState _stateBeforeShowPopup;

        private int _currentLevel;

        public void Awake()
        {
            initSettings();
        }

        public IEnumerator Start()
        {
            _currentLevel = 1;

            _gameState = GameState.Init;

            yield return new WaitForEndOfFrame();
        }

        public void Update()
        {
            if (_gameState == GameState.Running)
            {
                // GameplayMonoBehaviour.Execute();
            }
        }

        private void initSettings()
        {
            Application.targetFrameRate = 60;
        }

        private void showPopup(Popups.IPopup popup)
        {
            _stateBeforeShowPopup = _gameState;
            _gameState = GameState.Paused;
            popup.Show();
        }

        private void hidePopup(Popups.IPopup popup)
        {
            popup.Hide();
            _gameState = _stateBeforeShowPopup;
        }

        public enum GameState
        {
            Init,
            Running,
            Paused
        }
    }
}