using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Get_Character_DB : MonoBehaviour
{
    private Auth_Controller ac;

    private void Start()
    {
        if (GameObject.Find("Title_Console")) {
            ac = GameObject.Find("Title_Console").GetComponent<Auth_Controller>();
            ac.Get_Character_DB();
        } else {
            print("¿ÀÇÁ¶óÀÎ ÀÔ´Ï´Ù.");
        }
    }

    
}
