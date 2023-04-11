using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Athena.MiniGame.Fishdom2.InputManagers;
using Athena.MiniGame.Fishdom2.Data;
using Athena.MiniGame.Fishdom2.CameraManager;

namespace Athena.MiniGame.Fishdom2.GamePlay
{
    public class GameController : MonoBehaviour
    {
        [SerializeField] private GameView _gameView;
        [SerializeField] private InputManager _input;
        [SerializeField] private CameraController _cam;
        private LevelData _level;

        private int _currentState;
        private int _currentIndex;
        private int _previousIndex;

        public void Initialize()
        {
            _level = G.DataService.GetLevelData();
            SetUpCurrent();
            _gameView.Initialize(_level);
        }

        public void Update()
        {
            _input.Execute();
        }

        public void ProcessingInput(int clickedIndex)
        {
            bool isLock = _gameView.childObjects[clickedIndex].GetComponent<TileStatus>().IsLock;
            int newValue = _level.tile[clickedIndex].Value;
            int currentValue = _level.tile[CurrentIndex].Value;

            if (clickedIndex < 0 || clickedIndex == CurrentIndex || isLock) return;

            if(currentValue <= newValue)
            {
                Debug.Log("GameOver");
            }
            else
            {
                UpdateData(clickedIndex,currentValue);
                if(IsGameStateChange(clickedIndex))
                {
                    UpdateCamera();
                }
                if(IsWin())
                {
                    Debug.Log("Win!!!");
                }
            }
        }
        public void UpdateCamera()
        {
            _cam.UpdateCameraState(CurrentState, _level);
        }

        public void UpdateData(int clickedIndex, int currentValue)
        {
            PreviousIndex = CurrentIndex;
            CurrentIndex = clickedIndex;
            string calculation = _level.tile[CurrentIndex].calculation;
            switch (calculation)
            {
                case "+":
                    _level.tile[CurrentIndex].Value += currentValue;
                    _level.tile[PreviousIndex].Value = 0;
                    break;
                case "x":
                    _level.tile[CurrentIndex].Value *= currentValue;
                    _level.tile[PreviousIndex].Value = 0;
                    break;
                default:
                    break;
            }
            int newValue = _level.tile[CurrentIndex].Value;

            StartCoroutine(_gameView.StartUpdateView(CurrentIndex, currentValue, newValue));
        }
        public bool IsGameStateChange(int clickedIndex)
        {
            if(_level.Tile[clickedIndex].StateChangeFlag == 1)
            {
                CurrentState++;
                _gameView.UpdateTileState(_level);
                return true;
            }
            return false;
        }

        public bool IsWin()
        {
            return CurrentIndex == _level.TotalTile - 1;
        }

        public void SetUpCurrent()
        {
            _currentIndex = _level.FirstIndex;
            _currentState = 1; //First game state
        }

        public int CurrentState
        {
            get => _currentState;
            set => _currentState = value;
        }

        public int CurrentIndex
        {
            get => _currentIndex;
            set => _currentIndex = value;
        }
        public int PreviousIndex
        {
            get => _previousIndex;
            set => _previousIndex = value;
        }
    }

}
