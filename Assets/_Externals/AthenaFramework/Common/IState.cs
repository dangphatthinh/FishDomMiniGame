using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Athena.Common
{
    public interface IState 
    {
        public bool IsInitialized { get; }
        void Exit();
        void Initialize();
        void Resume();
        void Clear();
    }
}