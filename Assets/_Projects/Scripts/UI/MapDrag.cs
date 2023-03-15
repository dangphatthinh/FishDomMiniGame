using UnityEngine;
using System;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI
{
    public class MapDrag : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
    {
        public event Action OnBeginDragEvent;
        public event Action OnDragEvent;
        public event Action OnEndDragEvent;

        public float rectWidth;
        public float rectHeight;

        private ScrollRect _mapScrollRect;

        void Start()
        {
            _mapScrollRect = GetComponent<ScrollRect>();
            rectWidth = GetComponent<RectTransform>().rect.width;
            rectHeight = GetComponent<RectTransform>().rect.height;
        }

        public void OnDrag(PointerEventData eventData)
        {
            OnDragEvent?.Invoke();
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            OnBeginDragEvent?.Invoke();
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            OnEndDragEvent?.Invoke();
        }
    }
}
