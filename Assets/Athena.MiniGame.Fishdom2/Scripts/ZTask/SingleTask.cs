using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Athena.MiniGame.Fishdom2.ZTask
{
    public class SingleTask : IEnumerable
    {
        public event Action OnComplete;

        private IEnumerator _enumerator;

        public SingleTask(IEnumerator enumerator)
        {
            _enumerator = enumerator;
        }

        public IEnumerator GetEnumerator()
        {
            if (_enumerator != null)
            {
                Stack<IEnumerator> stack = new Stack<IEnumerator>();
                IEnumerator task = _enumerator;
                stack.Push(task);
                while (stack.Count > 0)
                {
                    IEnumerator ce = stack.Peek();
                    if (!ce.MoveNext())
                    {
                        stack.Pop();
                    }
                    else if (ce.Current != ce && ce.Current != null)
                    {
                        if (ce.Current is IEnumerable)
                        {
                            stack.Push(((IEnumerable)ce.Current).GetEnumerator());
                        }
                        else if (ce.Current is IEnumerator)
                        {
                            stack.Push(ce.Current as IEnumerator);
                        }
                        else if (ce.Current is WWW)
                        {
                            stack.Push(new SingleTask.WWWEnumerator(ce.Current as WWW));
                        }
                    }
                    yield return null;
                }
                _enumerator = null;
            }
            if (OnComplete != null)
            {
                OnComplete();
            }
            yield break;
        }

        private class WWWEnumerator : IEnumerator
        {
            private WWW _www;

            public WWWEnumerator(WWW www)
            {
                _www = www;
            }

            public object Current
            {
                get
                {
                    return _www;
                }
            }

            public bool MoveNext()
            {
                return !_www.isDone;
            }

            public void Reset()
            {
            }
        }
    }
}
