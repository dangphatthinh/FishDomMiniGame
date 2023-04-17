using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Athena.MiniGame.Fishdom2.Data
{
    [System.Serializable]
    public class GameData 
    {
        public bool Result; // true === win, false === lose
        public int Star;
        public int Score;
        public List<int> Data = new List<int>();

        public GameData(bool result, int star, int score)
        {
            Result = result;
            Star = star;
            Score = score;
        }
        public GameData() { }

        public void SetGameData(LevelData data)
        {
            for(int i = 0; i < data.TotalTile; i++)
            {
                Data.Add(data.Tile[i].Value);
            }
        }
    }
}

