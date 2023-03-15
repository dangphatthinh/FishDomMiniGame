using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class XList<T> : MonoBehaviour
        where T : MonoBehaviour
    {
        public event Action<T> OnItemAdded;
        public event Action<T> OnItemRemoved;

        [SerializeField]
        private T _itemPrefab = null;
        [SerializeField]
        protected Transform _parent = null;

        private List<T> _items = new List<T>();

        public int Count { get { return _items.Count; } }

        public T GetItem(int idx)
        {
            return getItem(idx);
        }

        protected T getItem(int idx)
        {
            return _items[idx];
        }

        protected T addItem()
        {
            var item = Instantiate(_itemPrefab);
            item.gameObject.SetActive(true);
            item.transform.SetParent(_parent, false);
            _items.Add(item);
            notifyItemAdded(item);
            return item;
        }

        protected void removeItem(int i)
        {
            var item = _items[i];
            _items.Remove(item);
            notifyItemRemoved(item);
            // ZPool.Assets.Destroy(item);
            UnityEngine.GameObject.Destroy(item.gameObject);
        }

        protected void removeAllItems()
        {
            for (int i = _items.Count - 1; i >= 0; --i)
            {
                removeItem(i);
            }
            _items.Clear();
        }

        private void notifyItemAdded(T item)
        {
            if (OnItemAdded != null) OnItemAdded(item);
        }

        private void notifyItemRemoved(T item)
        {
            if (OnItemRemoved != null) OnItemRemoved(item);
        }
    }
}
