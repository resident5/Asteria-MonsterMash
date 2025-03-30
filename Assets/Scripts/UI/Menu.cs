using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public int level;
    public Button firstButton;
    public Transform holder;

    public void SetActive(bool active)
    {
        gameObject.SetActive(active);
    }
}
