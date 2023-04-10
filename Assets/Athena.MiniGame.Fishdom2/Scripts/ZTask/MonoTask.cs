using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Athena.MiniGame.Fishdom2.ZTask
{
    public class MonoTask : MonoBehaviour
    {
        private List<IEnumerator> _enumerators;
        private Queue<Action> _executeOnMainThreadQueue = new Queue<Action>();

        public bool Paused { private get; set; }

        public void Awake()
        {
            _enumerators = new List<IEnumerator>();
            Paused = false;
        }

        public void StartCoroutineManaged(IEnumerator task)
        {
            _enumerators.Add(new SingleTask(task).GetEnumerator());
        }

        public void ExecuteOnMainThread(Action action)
        {
            _executeOnMainThreadQueue.Enqueue(action);
        }

        private void FixedUpdate()
        {
            if (!Paused)
            {
                _enumerators.RemoveAll((IEnumerator enumerator) => !enumerator.MoveNext());
            }
        }

        public void Update()
        {
            while (_executeOnMainThreadQueue.Count > 0)
            {
                Action action = _executeOnMainThreadQueue.Dequeue();
                if (action != null)
                {
                    action();
                }
                else
                {
                    // Debugger.LogWarning("MonoTask.Update executeOnMainThreadQueue action is null");
                }
            }
        }
    }
}
