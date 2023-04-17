using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Tool
{
    public class CreateTile : MonoBehaviour
    {
        [SerializeField] private Transform _tileHolder;
        [SerializeField] string _tilePath = "Fishdom2/Prefabs/Test/Tile";
        private int index = 0;
        public List<GameObject> _listTile;
        

        public void Create()
        {
            GameObject tile = Resources.Load<GameObject>(_tilePath);
            GameObject instance = Instantiate(tile, _tileHolder);
            instance.AddComponent<TileConfig>()._index = index;
            instance.name = "Tile " + index++;
            _listTile.Add(instance);
        }
    }
}

