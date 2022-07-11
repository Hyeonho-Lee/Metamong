using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World_Controller : MonoBehaviour
{
    public string username;
    public string uid;

    private void Update()
    {
        if (GameObject.Find("Title_Console"))
        {
            GameObject empty = GameObject.Find("Title_Console");
            username = empty.gameObject.GetComponent<Auth_Controller>().userName;
            uid = empty.gameObject.GetComponent<Auth_Controller>().localId;
            Destroy(empty);
        }
    }
}
