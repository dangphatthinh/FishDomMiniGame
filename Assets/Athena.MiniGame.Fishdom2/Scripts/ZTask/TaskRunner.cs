using System;
using System.Collections;
using UnityEngine;

namespace Athena.MiniGame.Fishdom2.ZTask
{
    public class TaskRunner
    {
        private MonoTask _runner;
        private static TaskRunner _instance;

        public static TaskRunner Instance
        {
            get
            {
                if (_instance == null)
                {
                    initInstance();
                }
                return _instance;
            }
        }

        public Coroutine Run(IEnumerable task)
        {
            return this.Run(task.GetEnumerator());
        }

        public Coroutine Run(IEnumerator task)
        {
            if (this._runner != null && this._runner.enabled)
            {
                this._runner.gameObject.SetActive(true);
                return this._runner.StartCoroutine(task);
            }
            return null;
        }

        public void RunSync(IEnumerable task)
        {
            this.RunSync(task.GetEnumerator());
        }

        public void RunSync(IEnumerator task)
        {
            while (task.MoveNext())
            {
            }
        }

        public void RunManaged(IEnumerable task)
        {
            this.RunManaged(task.GetEnumerator());
        }

        public void RunManaged(IEnumerator task)
        {
            if (_runner != null && _runner.enabled)
            {
                _runner.gameObject.SetActive(true);
                _runner.StartCoroutineManaged(task);
            }
        }

        public void ExecuteOnMainThread(Action act)
        {
            _runner.ExecuteOnMainThread(act);
        }

        public void Destroy()
        {
            Stop();
            if (_runner != null)
            {
                if (Application.isPlaying)
                {
                    UnityEngine.Object.Destroy(_runner.gameObject);
                }
                else
                {
                    UnityEngine.Object.DestroyImmediate(_runner.gameObject);
                }
            }
            _instance = null;
        }

        public void Stop()
        {
            if (_runner != null)
            {
                _runner.StopAllCoroutines();
            }
        }

        public void PauseManaged()
        {
            _runner.Paused = true;
        }

        public void ResumeManaged()
        {
            _runner.Paused = false;
        }

        private static void initInstance()
        {
            var gameObject = new GameObject("_TaskRunner");
            UnityEngine.Object.DontDestroyOnLoad(gameObject);
            _instance = new TaskRunner();
            _instance._runner = gameObject.AddComponent<MonoTask>();
        }
    }
}
