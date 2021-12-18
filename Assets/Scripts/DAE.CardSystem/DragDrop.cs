using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragDrop : MonoBehaviour, /*IPointerDownHandler,*/ IBeginDragHandler, IEndDragHandler, IDragHandler
{
    //[SerializeField]
    //private Canvas _canvas;

    //private RectTransform _rectTransform;
    //private CanvasGroup _canvasGroup;

    //private void Awake()
    //{
    //    _rectTransform = GetComponent<RectTransform>();
    //    _canvasGroup = GetComponent<CanvasGroup>();
    //}

    //public void OnBeginDrag(PointerEventData eventData)
    //{
    //    //Debug.Log("OnBeginDrag");
    //    _canvasGroup.alpha = 0.75f;
    //    _canvasGroup.blocksRaycasts = false;

    //}

    //public void OnDrag(PointerEventData eventData)
    //{
    //    //Debug.Log("OnDrag");
    //    _rectTransform.anchoredPosition += eventData.delta / _canvas.scaleFactor;
    //}

    //public void OnEndDrag(PointerEventData eventData)
    //{
    //    //Debug.Log("OnEndDrag");
    //    _canvasGroup.alpha = 1f;
    //    _canvasGroup.blocksRaycasts = true;

    //}

    //public void OnPointerDown(PointerEventData eventData)
    //{
    //    Debug.Log("OnPointerDown");
    //}

    public bool dragOnSurfaces = true;

    private GameObject m_DraggingIcon;
    private RectTransform m_DraggingPlane;
    private CanvasGroup m_canvasGroup;

    [SerializeField]
    private HorizontalLayoutGroup _layOutGroup;

    private void Awake()
    {
        m_canvasGroup = GetComponent<CanvasGroup>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        var canvas = FindInParents<Canvas>(gameObject);
        if (canvas == null)
            return;

        // We have clicked something that can be dragged.
        // What we want to do is create an icon for this.
        //m_DraggingIcon = new GameObject("icon");
        m_DraggingIcon = gameObject;

        m_DraggingIcon.transform.SetParent(canvas.transform, false);
        //m_DraggingIcon.transform.SetParent(this.transform, true);
        m_DraggingIcon.transform.SetAsLastSibling();
        //m_DraggingIcon = gameObject;

        //var image = m_DraggingIcon.AddComponent<Image>();

        //image.sprite = GetComponent<Image>().sprite;
        m_canvasGroup.alpha = 0.5f;
        //image.SetNativeSize();

        if (dragOnSurfaces)
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
        if (dragOnSurfaces && data.pointerEnter != null && data.pointerEnter.transform as RectTransform != null)
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
            m_DraggingIcon.transform.SetParent(_layOutGroup.transform, true);
            m_canvasGroup.alpha = 1f;
        }
        //Destroy(m_DraggingIcon);


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

