using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room_Cameras : MonoBehaviour
{
    public Vector3 dir_nor;

    private GameObject Main_Camera;

    private Player_Console PC;

    private void Start()
    {
        Main_Camera = this.gameObject;

        PC = GameObject.Find("Room_Console").GetComponent<Player_Console>();
        dir_nor = new Vector3(1, 0, 1);
    }

    private void Update()
    {
        Rotate_Canvas();
    }

    public void Rotate_Canvas()
    {
        for (int i = 0; i < PC.all_player.Length; i++) {
            if (PC.all_player[i] != null) {
                Transform Player_Canvas = PC.all_player[i].transform.GetChild(9).transform;
                if (dir_nor != Vector3.zero) {
                    Player_Canvas.rotation = Quaternion.LookRotation(dir_nor);
                }
            }
        }

        for (int i = 0; i < PC.All_Canvas.Length; i++) {
            if (PC.All_Canvas[i] != null) {
                Transform All_Canvas = PC.All_Canvas[i].transform;
                if (dir_nor != Vector3.zero) {
                    All_Canvas.rotation = Quaternion.LookRotation(dir_nor);
                }
            }
        }
    }
}
