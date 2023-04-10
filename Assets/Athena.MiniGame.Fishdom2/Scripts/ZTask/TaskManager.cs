using System;
using System.Collections;
using UnityEngine;

namespace Athena.MiniGame.Fishdom2.ZTask
{
    public static class TaskManager
    {
        public static void DoNextFrame(Action act, int yieldCount = 1)
        {
            TaskRunner.Instance.Run(yieldThenAction(act, yieldCount));
        }

        public static void DoSecondsAfter(Action act, float sec)
        {
            TaskRunner.Instance.Run(waitThenAction(act, sec));
        }

        public static Coroutine StartCoroutine(IEnumerator coR)
        {
            return TaskRunner.Instance.Run(coR);
        }

        public static void ExecuteOnMainThread(Action act)
        {
            TaskRunner.Instance.ExecuteOnMainThread(act);
        }

        private static IEnumerator yieldThenAction(Action act, int yieldCount)
        {
            int iterations = 0;
            while (iterations < yieldCount)
            {
                iterations++;
                yield return null;
            }
            act();
            yield break;
        }

        private static IEnumerator waitThenAction(Action act, float waitSec)
        {
            yield return new WaitForSeconds(waitSec);
            act();
            yield break;
        }
    }
}
