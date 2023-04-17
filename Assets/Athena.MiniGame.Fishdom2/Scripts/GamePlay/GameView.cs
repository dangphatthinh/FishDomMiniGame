using System.Collections;
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
        [SerializeField] private Sprite mainCharacter;

        public void Initialize(LevelData data) 
        {
            CreatTileMap(data);
            CreatNumberObject(data);
            _gameController.UpdateCamera();
            UpdateTileState(data);
            UpdateMainCharacter(_gameController.CurrentIndex);
        }

        public void CreatTileMap(LevelData data)
        {
            GameObject[] prefabs = Resources.LoadAll<GameObject>("Fishdom2/Prefabs/Tile");
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

        public void CreatNumberObject(LevelData data)
        {
            GameObject[] numberPrefab = Resources.LoadAll<GameObject>("Fishdom2/Prefabs/TextObject");
            for (int i = 0; i < data.totalTile; i++)
            {
                GameObject numberInstance = Instantiate(numberPrefab[data.Tile[i].TileType - 1], _textHolder);
                Vector3 pos = new Vector3(data.Tile[i].XPos, data.Tile[i].YPos, 0);
                numberInstance.transform.position = pos;
                Text newText = numberInstance.transform.GetChild(0).GetComponent<Text>();
                numberInstance.GetComponent<RectTransform>().localScale = Vector3.one*3;
                newText.text = (data.Tile[i].calculation == "+"?null: data.Tile[i].calculation.ToString()) + data.Tile[i].value.ToString();
                textObjects.Add(numberInstance);
            }
        }

        public IEnumerator StartUpdateView(int oldIndex, int newIndex, int oldValue, int newValue)
        {
            CharacterMoveAnimation(oldIndex, newIndex);
            yield return new WaitForSeconds(0.5f);
            UpdateView(oldIndex, newIndex, oldValue, newValue);
        }

        public void CharacterMoveAnimation(int oldIndex, int newIndex)
        {
            Vector3 target = textObjects[newIndex].transform.position;
            textObjects[oldIndex].transform.DOMove(target, 0.5f).SetEase(Ease.OutQuad);
        }

        public void UpdateView(int oldIndex, int newIndex, int oldValue,int newValue)
        {
            RemoveOldObject(oldIndex);
            UpdateNewObject(newIndex, oldValue, newValue);
        }

        public void RemoveOldObject(int oldIndex)
        {
            textObjects[oldIndex].transform.GetChild(0).GetComponent<Text>().text = "";
            textObjects[oldIndex].transform.GetChild(1).gameObject.SetActive(false);
            childObjects[oldIndex].GetComponent<SpriteRenderer>().DOColor(lockedColor, 0.5f).SetEase(Ease.OutQuad);
            childObjects[oldIndex].GetComponent<TileStatus>().IsLock = true;
        }

        public void UpdateNewObject(int newIndex, int oldValue, int newValue)
        {
            UpdateMainCharacter(newIndex);
            childObjects[newIndex].GetComponent<SpriteRenderer>().color = Color.green;
            DOTween.To(() => oldValue, x => {
                oldValue = x;
                textObjects[newIndex].transform.GetChild(0).GetComponent<Text>().text = oldValue.ToString();
            }, newValue, 0.5f);
        }
        public void UpdateMainCharacter(int newIndex)
        {
            textObjects[newIndex].transform.GetChild(1).GetComponent<Image>().sprite = mainCharacter;
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
