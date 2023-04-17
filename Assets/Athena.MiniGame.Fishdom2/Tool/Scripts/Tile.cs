using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tool
{
    public class Tile : MonoBehaviour
    {
        public void OnOpenSettingUI()
        {
            UIController.Instance.CurrentIndex = this.gameObject.GetComponent<TileConfig>()._index;
            UIController.Instance.OpenSettingUI();    
        }
    }

}

