using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Tool
{
    public class SettingUI : MonoBehaviour
    {
        [SerializeField] private CreateTile Tiles;
        [SerializeField] private TMP_InputField xPos;
        [SerializeField] private TMP_InputField yPos;
        [SerializeField] private TMP_InputField width;
        [SerializeField] private TMP_InputField height;
        [SerializeField] private TMP_InputField value;


        public void OnUpdatexPos()
        {
            float x = float.Parse(xPos.text);
            float y = Tiles._listTile[UIController.Instance.CurrentIndex].transform.position.y;
            float z = Tiles._listTile[UIController.Instance.CurrentIndex].transform.position.z;
            Tiles._listTile[UIController.Instance.CurrentIndex].GetComponent<TileConfig>()._xPos = x;
            Tiles._listTile[UIController.Instance.CurrentIndex].transform.position = new Vector3(x, y, z);
        }
        public void OnUpdateyPos()
        {
            float x = Tiles._listTile[UIController.Instance.CurrentIndex].transform.position.x;
            float y = float.Parse(yPos.text);
            float z = Tiles._listTile[UIController.Instance.CurrentIndex].transform.position.z;
            Tiles._listTile[UIController.Instance.CurrentIndex].GetComponent<TileConfig>()._yPos = y;
            Tiles._listTile[UIController.Instance.CurrentIndex].transform.position = new Vector3(x, y, z);
        }
        public void OnUpdateWidth()
        {
            float w = float.Parse(width.text);
            float h = Tiles._listTile[UIController.Instance.CurrentIndex].GetComponent<RectTransform>().sizeDelta.y;
            Tiles._listTile[UIController.Instance.CurrentIndex].GetComponent<TileConfig>()._width = w;
            Tiles._listTile[UIController.Instance.CurrentIndex].GetComponent<RectTransform>().sizeDelta = new Vector2(w, h);
        }
        public void OnUpdateHeight()
        {
            float w = Tiles._listTile[UIController.Instance.CurrentIndex].GetComponent<RectTransform>().sizeDelta.x;
            float h = float.Parse(height.text);
            Tiles._listTile[UIController.Instance.CurrentIndex].GetComponent<TileConfig>()._height = h;
            Tiles._listTile[UIController.Instance.CurrentIndex].GetComponent<RectTransform>().sizeDelta = new Vector2(w, h);
        }
        public void OnUpdateValue()
        {
            Tiles._listTile[UIController.Instance.CurrentIndex].GetComponent<TileConfig>().value = int.Parse(value.text);
        }
    }

}
