using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Athena.MiniGame.Fishdom2.Data
{
    public class TileStatus : MonoBehaviour
    {
        private int _index;
        private bool _isLock = true;

        public int Index
        {
            get => _index;
            set => _index = value;
        }
        public bool IsLock
        {
            get => _isLock;
            set => _isLock = value;
        }

        public TileStatus(int index)
        {
            _index = index;
            _isLock = true;
        }
    }

}
