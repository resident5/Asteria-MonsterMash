using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class SelectablePosition : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("Cursor entered " + gameObject);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("Cursor exited " + gameObject);
    }
}
