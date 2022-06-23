using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World_Controller : MonoBehaviour
{
    public List<Auth_Controller> auth_controller;

    private void Update()
    {
        if (GameObject.Find("Title_Console"))
        {
            GameObject empty = GameObject.Find("Title_Console");
            auth_controller.Add(empty.gameObject.GetComponent<Auth_Controller>());
            Destroy(empty);
        }
    }
}
