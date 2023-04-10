using System.Collections;
using UnityEngine;

namespace Athena.MiniGame.Fishdom2
{
    public class G
    {
        private static Services.DataService _dataService;

        public static Services.DataService DataService { get { return _dataService; } }

        public static IEnumerator Init()
        {
            _dataService = new Services.DataService();
            _dataService.Init();
            yield return new WaitUntil(() => _dataService.HasInit);
            yield return new WaitForSeconds(1f);
            yield return new WaitForEndOfFrame();
        }
    }
}

