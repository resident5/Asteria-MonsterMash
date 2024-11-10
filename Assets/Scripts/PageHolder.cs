using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PageHolder : MonoBehaviour
{
    private GameObject[] pages;
    public GameObject previousActivePage;

    public void SetPages()
    {
        pages = new GameObject[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            pages[i] = transform.GetChild(i).gameObject;
        }
    }
}