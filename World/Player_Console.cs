using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Console : MonoBehaviour
{
    public GameObject[] all_player;

    private float update_realtime;
    private float update_time;

    private bool is_update;

    private void Start()
    {
        update_time = 1.0f;

        Update_PlayerList();
        //Update_Custom();
    }

    private void Update()
    {
        if (!is_update) {
            if (update_realtime <= update_time) {
                update_realtime += Time.deltaTime;
            } else {
                is_update = true;
                Update_PlayerList();
                //Update_Custom();
            }
        }
    }

    public void Update_PlayerList()
    {
        update_realtime = 0.0f;
        is_update = false;
        all_player = new GameObject[1];
        all_player = GameObject.FindGameObjectsWithTag("Player");
    }

    public void Update_Custom()
    {
        for (int i = 0; i < all_player.Length; i++) {
            Customize_Character CC = all_player[i].GetComponent<Customize_Character>();
            CC.Change_Character("Top_Part", "top");
        }
    }
}
