using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Athena.MiniGame.Fishdom2.Data;

namespace Athena.MiniGame.Fishdom2.Services
{
    public class DataService 
    {
        private LevelData _levels;

        private bool _hasInit;

        public bool HasInit { get { return _hasInit; } }
        public  LevelData Levels { get { return _levels; } }

        public void Init()
        {
            _hasInit = false;
            ZTask.TaskManager.StartCoroutine(init());
        }

        private IEnumerator init()
        {
            var dataPath = "Fishdom2/data/levelmap";
            var jsonFile = Resources.LoadAsync<TextAsset>(dataPath);
            yield return jsonFile;
            var jsonObj = GenericsJSONParser.JsonDecode(((TextAsset)jsonFile.asset).text);
            _levels = new LevelData(jsonObj);
            Resources.UnloadAsset(jsonFile.asset);
            _hasInit = true;
        }
        public LevelData GetLevelData()
        {
            return _levels;
        }
    }

}
