using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room_Console : MonoBehaviour
{
    public GameObject WallPanel;
    public GameObject GroundPanel;

    public void Click_Wall()
    {
        WallPanel.SetActive(false);
    }
}
