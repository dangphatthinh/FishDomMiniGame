using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Athena.MiniGame.Fishdom2.InputManagers;
using Athena.MiniGame.Fishdom2.Data;
using Athena.MiniGame.Fishdom2.CameraManager;

namespace Athena.MiniGame.Fishdom2.GamePlay
{
    public class GameController : MonoBehaviour, ITileStateListener
    {
        [SerializeField] private GameView _gameView;
        [SerializeField] private InputManager _input;
        [SerializeField] private CameraController _cam;

        private LevelData _level;
        private GameData _gameData = new GameData();

        private int _currentState;
        private int _currentIndex;
        private int _previousIndex;

        private IGameStateListener _gameStateListener;

        public void Initialize()
        {
            _level = G.DataService.GetLevelData();
            _gameData.SetGameData(_level);
            SetUpCurrent();
            _gameView.Initialize(_level);
            _cam.SetTileStateListener(this);
        }

        public void Update()
        {
            _input.Execute();
            if(_input.InputEneble)
            {
                if (IsTriggerNextStep())
                {
                    ProcessingInput(CurrentIndex + 1);
                }
            }
        }

        public void ProcessingInput(int clickedIndex)
        {
            StartCoroutine(BlockInput());
            bool isLock = _gameView.childObjects[clickedIndex].GetComponent<TileStatus>().IsLock;
            int newValue = _gameData.Data[clickedIndex];
            int currentValue = _gameData.Data[CurrentIndex];
            int oldIndex = CurrentIndex;

            if (clickedIndex < 0 || clickedIndex == CurrentIndex || isLock) return;
            if(currentValue > newValue)
            {
                UpdateData(clickedIndex, currentValue);
                int newValueUpdated = _gameData.Data[CurrentIndex];
                UpdateView(clickedIndex, currentValue, newValueUpdated);
                CheckWin();
            }
            else
            {
                StartCoroutine(_gameView.StartUpdateView(_level,oldIndex, clickedIndex, currentValue, 0));
                if (_gameStateListener != null) _gameStateListener.OnGameComplete();
            }
        }
        public void SetGameStateListener(IGameStateListener listener)
        {
            _gameStateListener = listener;
        }
        public void UpdateCamera()
        {
            _cam.UpdateCameraState(CurrentState, _level);
        }
        public void UpdateView(int clickedIndex, int currentValue, int newValue)
        {
            StartCoroutine(_gameView.StartUpdateView(_level,PreviousIndex, CurrentIndex, currentValue, newValue));
            if (IsGameStateChange(clickedIndex))
            {
                UpdateCamera();
            }
        }

        public void UpdateData(int clickedIndex, int currentValue)
        {
            PreviousIndex = CurrentIndex;
            CurrentIndex = clickedIndex;
            string calculation = _level.tile[CurrentIndex].calculation;

            switch (calculation)
            {
                case "+":
                    _gameData.Data[CurrentIndex] += currentValue;
                    _gameData.Data[PreviousIndex] = 0;
                    break;
                case "x":
                    _gameData.Data[CurrentIndex] *= currentValue;
                    _gameData.Data[PreviousIndex] = 0;
                    break;
                default:
                    break;
            }
                     
        }
        public bool IsGameStateChange(int clickedIndex)
        {
            if(_level.Tile[clickedIndex].StateChangeFlag == 1)
            {
                CurrentState++;
                return true;
            }
            return false;
        }
        public void CheckWin()
        {
            if(IsWin())
            {
                _gameData.Score = _gameData.Data[CurrentIndex];
                if (_gameStateListener != null) _gameStateListener.OnGameComplete();
            }
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
        public GameData GetGameData()
        {
            return _gameData;
        }
        public void OnTileStateChange()
        {
            _gameView.UpdateTileState(_level);
        }
        public void EnebleInput()
        {
            _input.InputEneble = true;
        }
        public bool IsTriggerNextStep()
        {
            return _level.Tile[CurrentIndex].StateChangeFlag == 2;
        }
        IEnumerator BlockInput()
        {
            _input.InputEneble = false;
            yield return new WaitForSeconds(1.2f);
            _input.InputEneble = true;
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
