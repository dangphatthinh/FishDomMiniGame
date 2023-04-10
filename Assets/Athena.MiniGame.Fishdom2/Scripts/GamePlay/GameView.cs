using System;
using System.Collections.Generic;
using UnityEngine;
using Athena.MiniGame.Fishdom2.Data;
using UnityEngine.UI;
using DG.Tweening;

namespace Athena.MiniGame.Fishdom2.GamePlay
{
    public class GameView : MonoBehaviour
    {
        [SerializeField] private GameController _gameController;
        [SerializeField] private Transform _objectHolder;
        [SerializeField] private Transform _textHolder;

        public List<GameObject> childObjects = new List<GameObject>();
        public List<GameObject> textObjects = new List<GameObject>();

        private Color lockedColor = new Color32(84, 143, 147, 200);
        private Color unlockedColor = new Color32(158, 236, 241, 200);

        public void Initialize(LevelData data) 
        {
            CreatTileObject(data);
            CreatTextObject(data);
            _gameController.UpdateCamera();
            UpdateTileState(data);
        }
        public void CreatTileObject(LevelData data)
        {
            GameObject[] prefabs = Resources.LoadAll<GameObject>("Prefabs/Tile");
            for (int i = 0; i < data.totalTile; i++)
            {
                GameObject instance = Instantiate(prefabs[data.Tile[i].TileType - 1], _objectHolder);
                Vector3 pos = new Vector3(data.Tile[i].XPos, data.Tile[i].YPos, 0);
                instance.transform.position = pos;
                instance.name = "Tile" + i;
                instance.AddComponent<TileStatus>().Index = i;
                instance.GetComponent<TileStatus>().IsLock = true;
                childObjects.Add(instance);
            }
        }
        public void CreatTextObject(LevelData data)
        {
            GameObject textPrefab = Resources.Load<GameObject>("Prefabs/Text");
            for (int i = 0; i < data.totalTile; i++)
            {
                GameObject textInstance = Instantiate(textPrefab, _textHolder);
                Vector3 pos = new Vector3(data.Tile[i].XPos, data.Tile[i].YPos, 0);
                textInstance.transform.position = pos;
                Text newText = textInstance.GetComponent<Text>();
                textInstance.GetComponent<RectTransform>().localScale = Vector3.one * 5;
                newText.text = data.Tile[i].calculation.ToString() + data.Tile[i].value.ToString();
                textObjects.Add(textInstance);
            }
        }
        public void UpdateView(int index, int value)
        {
            textObjects[index].GetComponent<Text>().text = value.ToString();
            childObjects[index].GetComponent<SpriteRenderer>().color = Color.green;
            textObjects[_gameController.CurrentIndex].GetComponent<Text>().text = "";
            childObjects[_gameController.CurrentIndex].GetComponent<SpriteRenderer>().DOColor(lockedColor, 0.5f).SetEase(Ease.OutQuad);
        }
        public void UpdateTileState(LevelData data)
        {
            for (int i = 0; i < data.TotalTile; i++)
            {
                if (data.Tile[i].State == _gameController.CurrentState)
                {
                    if(i == _gameController.CurrentIndex)
                    {
                        childObjects[_gameController.CurrentIndex].GetComponent<SpriteRenderer>().color = Color.green;
                    }
                    else
                    {
                        childObjects[i].GetComponent<TileStatus>().IsLock = false;
                        childObjects[i].GetComponent<SpriteRenderer>().DOColor(unlockedColor, 0.5f).SetEase(Ease.OutQuad);
                    }
                }
            }         
        }
    }
}
