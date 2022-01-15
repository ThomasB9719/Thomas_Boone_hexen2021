﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DAE.HexesSystem;
using DAE.GameSystem.Cards;

namespace DAE.GameSystem
{
    class Draggable: MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
    {
        public bool DragOnSurfaces = true;

        private GameObject m_DraggingIcon;
        private RectTransform m_DraggingPlane;
        private CanvasGroup m_canvasGroup;

        [SerializeField]
        private HorizontalLayoutGroup _layOutGroup;

        private void Awake()
        {
            m_canvasGroup = GetComponent<CanvasGroup>();
            _layOutGroup = FindInParents<HorizontalLayoutGroup>(this.gameObject);
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            var canvas = FindInParents<Canvas>(gameObject);
            if (canvas == null)
                return;

            m_DraggingIcon = gameObject;            
            m_DraggingIcon.GetComponent<Image>().raycastTarget = false;

            m_DraggingIcon.transform.SetParent(canvas.transform, false);
            m_DraggingIcon.transform.SetAsLastSibling();
            m_canvasGroup.alpha = 0.5f;

            if (DragOnSurfaces)
            {
                m_DraggingPlane = transform as RectTransform;
            }
            else
                m_DraggingPlane = canvas.transform as RectTransform;

            SetDraggedPosition(eventData);
        }

        public void OnDrag(PointerEventData data)
        {
            if (m_DraggingIcon != null)
                SetDraggedPosition(data);
        }

        private void SetDraggedPosition(PointerEventData data)
        {
            if (DragOnSurfaces && data.pointerEnter != null && data.pointerEnter.transform as RectTransform != null)
                m_DraggingPlane = data.pointerEnter.transform as RectTransform;

            var rectTransform = m_DraggingIcon.GetComponent<RectTransform>();
            Vector3 globalMousePos;
            if (RectTransformUtility.ScreenPointToWorldPointInRectangle(m_DraggingPlane, data.position, data.pressEventCamera, out globalMousePos))
            {
                rectTransform.position = globalMousePos;
                rectTransform.rotation = m_DraggingPlane.rotation;
            }
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            if (m_DraggingIcon != null)
            {
                m_DraggingIcon.GetComponent<Image>().raycastTarget = true;
                m_DraggingIcon.transform.SetParent(_layOutGroup.transform, true);
                m_canvasGroup.alpha = 1f;
            }      
        }

        static public T FindInParents<T>(GameObject go) where T : Component
        {
            if (go == null) return null;
            var comp = go.GetComponent<T>();

            if (comp != null)
                return comp;

            Transform t = go.transform.parent;
            while (t != null && comp == null)
            {
                comp = t.gameObject.GetComponent<T>();
                t = t.parent;
            }
            return comp;
        }
    }
}
