using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character_Rotate : MonoBehaviour
{
    public GameObject Character;

    float y; //Character Rotate y

    public bool m_IsButtonDowning;

    public string Bnt_name;


    void Update()
    {

        if (m_IsButtonDowning)
        {
            switch (Bnt_name)
            {
                case "¿ÞÂÊ":
                    if (y == 360)
                    {
                        y = 0;
                    }

                    else
                    {
                        y += 1.0f;
                        Character.transform.localRotation = Quaternion.Euler(Character.transform.rotation.x, y, Character.transform.rotation.z);
                    }
                    break;

                case "¿À¸¥ÂÊ":
                    if (y == -360)
                    {
                        y = 0;
                    }

                    else
                    {
                        y -= 1.0f;
                        Character.transform.localRotation = Quaternion.Euler(Character.transform.rotation.x, y, Character.transform.rotation.z);
                    }
                    break;
            }
        }
    }

    public void PointerDown(string value)
    {
        if (Bnt_name == null)
        {
            Bnt_name = value;
        }
        m_IsButtonDowning = true;
    }

    public void PointerUp()
    {
        Bnt_name = null;
        m_IsButtonDowning = false;
    }


}