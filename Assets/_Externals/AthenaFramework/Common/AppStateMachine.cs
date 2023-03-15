using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Athena.Common
{
    public class AppStateMachine
    {
        public IState CurrentState
        {
            get
            {
                return _currentState;
            }
        }
        private IState _currentState;

        public AppStateMachine()
        {
            _currentState = null;
        }

        public IEnumerator SwitchProcess(IState newState)
        {
            IState previousState = _currentState;
            _currentState = newState;

            if (_currentState.IsInitialized)
            {
                //If current state is initialized, just call it's Resume
                _currentState.Resume();
            }
            else
            {
                //Call previous state's exit before the current state initializing
                previousState?.Exit();

                //Initialize current state
                _currentState.Initialize();

                //Wait for the initializing
                while (!_currentState.IsInitialized)
                    yield return null;

                //Call previous state's clear after current state initialized
                previousState?.Clear();
            }
        }
    }
}