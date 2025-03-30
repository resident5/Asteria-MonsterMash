using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContextMenuButton : MonoBehaviour
{
    /* Context Menu for:
     * Showing the item selection screen
     * Showing the spirit selection screen
     * Showing the 
     * 
     */
    public string context;

    public void ContextMenuSelector()
    {
        switch (context)
        {
            case "Select Spirit":
                break;
            case "Select Item":
                break;
            case "Examine":
                break;
            case "back":
                break;
        }
    }

    public void SelectItem()
    {

    }
}
