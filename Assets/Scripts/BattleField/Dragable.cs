
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Dragable : MonoBehaviour, IPointerClickHandler, IPointerDownHandler, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler
{
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    public GameObject parentObject;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        //parentObject = transform.parent.gameObject;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("onClick!!");
    }
        

        public void OnPointerDown(PointerEventData eventData)
    {
        //parentObject.transform.SetAsLastSibling();
        //Debug.Log("onClick!!");
        //throw new System.NotImplementedException();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        //Debug.Log("OnBeginDrag");
        canvasGroup.blocksRaycasts = false;
        //Debug.Log(eventData.delta);
    }

    public void OnDrag(PointerEventData eventData)
    {
        //Debug.Log("OnDrag");
        //transform.position = eventData.position;
        rectTransform.anchoredPosition += eventData.delta;

        //rectTransform.BroadcastMessage("Drag", transform, SendMessageOptions.DontRequireReceiver);
        //throw new System.NotImplementedException();
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = true;
        //eventData.delta = (0, 0);
        rectTransform.anchoredPosition = new Vector2(0, 0);
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            return;
        }
        throw new System.NotImplementedException();
    }
}
