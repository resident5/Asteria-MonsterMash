using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TWalkable : MonoBehaviour
{
    private void Start()
    {
        gameObject.layer = LayerMask.NameToLayer("Walkable");
    }
}
