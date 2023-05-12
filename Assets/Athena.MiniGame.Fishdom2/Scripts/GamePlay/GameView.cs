using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Athena.MiniGame.Fishdom2.Data;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

namespace Athena.MiniGame.Fishdom2.GamePlay
{
    public class GameView : MonoBehaviour
    {
        [SerializeField] private GameController _gameController;
        [SerializeField] private Transform _objectHolder;
        [SerializeField] private Transform _textHolder;
        [SerializeField] private Transform _charHolder;
        [SerializeField] private Transform _npcsHolder;
        private GameObject character;

        public List<GameObject> childObjects = new List<GameObject>();
        public List<GameObject> textObjects = new List<GameObject>();
        public List<GameObject> NPC = new List<GameObject>();

        private Color lockedColor = new Color32(84, 143, 147, 200);
        private Color unlockedColor = new Color32(158, 236, 241, 200);
                
        public void Initialize(LevelData data) 
        {
            CreateTileMap(data);
            CreateNumberObject(data);
            CreateMainCharater(data);
            CreateNPCs(data);
            _gameController.UpdateCamera();
            UpdateTileState(data);
        }

        public void CreateTileMap(LevelData data)
        {
            GameObject[] prefabs = Resources.LoadAll<GameObject>("Fishdom2/Prefabs/Tiles");
            for (int i = 0; i < data.totalTile; i++)
            {
                GameObject instance = Instantiate(prefabs[data.Tile[i].TileType - 1], _objectHolder);
                Vector3 pos = new Vector3(data.Tile[i].XPos , data.Tile[i].YPos , 0);
                instance.transform.position = Vector3.zero;
                instance.GetComponent<RectTransform>().anchoredPosition = pos;
                instance.name = "Tile" + i;
                instance.AddComponent<TileStatus>().Index = i;
                instance.GetComponent<TileStatus>().IsLock = true;
                childObjects.Add(instance);
            }
        }
        public void CreateNPCs(LevelData data)
        {
            GameObject[] prefabs = Resources.LoadAll<GameObject>("Fishdom2/Prefabs/NPCs");
            for (int i = 0; i < data.totalTile; i++)
            {
                if(i == _gameController.CurrentIndex)
                {
                    NPC.Add(null);
                    continue;
                }
                else if(data.Tile[i].calculation == "x")
                {
                    GameObject instance = Instantiate(prefabs[0], _charHolder);
                    Vector3 pos = new Vector3(data.CharPos[i].XCharPos, data.CharPos[i].YCharPos, 0);
                    instance.transform.position = pos;
                    instance.name = "NPC" + i;
                    NPC.Add(instance);
                }
                else if(data.Tile[i].calculation == "+")
                {
                    GameObject instance = Instantiate(prefabs[(int)Random.Range(1, 4)], _npcsHolder);
                    Vector3 pos = new Vector3(data.CharPos[i].XCharPos, data.CharPos[i].YCharPos, 0);
                    instance.transform.position = pos;
                    instance.name = "NPC" + i;
                    NPC.Add(instance);
                }                             
            }
        }

        public void CreateNumberObject(LevelData data)
        {
            GameObject numberPrefab = Resources.Load<GameObject>("Fishdom2/Prefabs/TextObject/TextObject");
            for (int i = 0; i < data.totalTile; i++)
            {
                GameObject numberInstance = Instantiate(numberPrefab, _textHolder);
                Vector3 pos = new Vector3(data.TextPos[i].XTextPos , data.TextPos[i].YTextPos, 0);
                numberInstance.GetComponent<RectTransform>().anchoredPosition = pos;
                TextMeshProUGUI newText = numberInstance.transform.GetComponent<TextMeshProUGUI>();
                numberInstance.GetComponent<RectTransform>().localScale = Vector3.one*2f;
                newText.text = (data.Tile[i].calculation == "+"?null: data.Tile[i].calculation.ToString()) + data.Tile[i].value.ToString();
                textObjects.Add(numberInstance);
            }
        }
        public void CreateMainCharater(LevelData data)
        {
            GameObject instance = Resources.Load<GameObject>("Fishdom2/Prefabs/MainCharacter");
            character = Instantiate(instance, _charHolder);
            Vector3 pos = new Vector3(data.CharPos[_gameController.CurrentIndex].XCharPos, data.CharPos[_gameController.CurrentIndex].YCharPos, 0);
            character.transform.position = pos;
            Vector3 textTarget = new Vector3(data.CharPos[_gameController.CurrentIndex].XCharPos - 0.8f, data.CharPos[_gameController.CurrentIndex].YCharPos + 0.6f, 0);
            textObjects[_gameController.CurrentIndex].transform.position = textTarget;
            textObjects[_gameController.CurrentIndex].GetComponent<TextMeshProUGUI>().color = Color.red;
        }

        public IEnumerator StartUpdateView(LevelData data,int oldIndex, int newIndex, int oldValue, int newValue)
        {
            CharacterMoveAnimation(data,oldIndex, newIndex);
            yield return new WaitForSeconds(0.5f);
            UpdateView(data, oldIndex, newIndex, oldValue, newValue);
        }

        public void CharacterMoveAnimation(LevelData data,int oldIndex, int newIndex)
        {
            Vector3 charTarget = new Vector3(data.CharPos[newIndex].XCharPos, data.CharPos[newIndex].YCharPos, 0);
            Vector3 textTarget = new Vector3(data.CharPos[newIndex].XCharPos -0.5f, data.CharPos[newIndex].YCharPos + 0.5f, 0);
            textObjects[oldIndex].transform.DOMove(textTarget, 0.5f).SetEase(Ease.OutQuad);
            Vector3 chartarget = new Vector3(charTarget.x, charTarget.y - 0.3f, charTarget.z); 
            character.transform.DOMove(chartarget, 0.5f).SetEase(Ease.OutQuad);
        }

        public void UpdateView(LevelData data,int oldIndex, int newIndex, int oldValue,int newValue)
        {
            RemoveOldObject(oldIndex);
            UpdateNewObject(data, newIndex, oldValue, newValue);
        }

        public void RemoveOldObject(int oldIndex)
        {
            textObjects[oldIndex].transform.GetComponent<TextMeshProUGUI>().text = "";
            childObjects[oldIndex].GetComponent<Image>().DOColor(lockedColor, 0.5f).SetEase(Ease.OutQuad);
            childObjects[oldIndex].GetComponent<TileStatus>().IsLock = true;
        }

        public void UpdateNewObject(LevelData data, int newIndex, int oldValue, int newValue)
        {
            childObjects[newIndex].GetComponent<Image>().color = Color.green;
            NPC[newIndex].gameObject.SetActive(false);
            /*DOTween.To(() => new Vector3(2.8f, 2.8f, 1f),
                   scale => textObjects[newIndex].transform.GetComponent<RectTransform>().gameObject.transform.localScale = scale,
                   new Vector3(3.1f, 3.1f, 1f),
                   0.25f).SetLoops(2);*/
            Vector3 textTarget = new Vector3(data.CharPos[newIndex].XCharPos - 0.5f, data.CharPos[newIndex].YCharPos + 0.5f, 0);
            textObjects[newIndex].transform.position = textTarget;
            textObjects[newIndex].GetComponent<TextMeshProUGUI>().color = Color.red;

            DOTween.To(() => oldValue, x => {
                oldValue = x;
                textObjects[newIndex].transform.GetComponent<TextMeshProUGUI>().text = oldValue.ToString();
            }, newValue, 0.5f);  
        }

        public void UpdateTileState(LevelData data)
        {
            for (int i = 0; i < data.TotalTile; i++)
            {
                if (data.Tile[i].State == _gameController.CurrentState)
                {
                    if(i == _gameController.CurrentIndex)
                    {
                        childObjects[_gameController.CurrentIndex].GetComponent<Image>().color = Color.green;
                    }
                    else
                    {
                        childObjects[i].GetComponent<TileStatus>().IsLock = false;
                        childObjects[i].GetComponent<Image>().DOColor(unlockedColor, 0.5f).SetEase(Ease.OutQuad);
                    }
                }
            }         
        }       
    }
}
