using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player_Cursor : MonoBehaviour
{
    public bool is_ctrl;

    public Sprite ctrl_on;
    public Sprite ctrl_off;
    public Image ctrl_image;

    public Sprite voice_on;
    public Sprite voice_off;
    public Sprite voice_load;
    public Image voice_image;

    private void Start()
    {
        is_ctrl = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl)) {
            if (!is_ctrl) {
                is_ctrl = true;
            }else {
                is_ctrl = false;
            }
        }

        if (is_ctrl) {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.Confined;
            ctrl_image.sprite = ctrl_on;
        } else {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            ctrl_image.sprite = ctrl_off;
        }
    }
}
