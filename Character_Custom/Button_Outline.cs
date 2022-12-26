using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Button_Outline : MonoBehaviour
{
    public int left, top;

    public List<GameObject> left_button = new List<GameObject>();
    public List<GameObject> face_button = new List<GameObject>();
    public List<GameObject> clothes_button = new List<GameObject>();
    public List<GameObject> accessory_button = new List<GameObject>();

    private void Start()
    {
        face_panel(0);
    }

    public void face_panel(int value)
    {
        left = 0;
        add_outline(left, value, face_button);
    }

    public void cloth_panel(int value)
    {
        left = 1;
        add_outline(left, value, clothes_button);
    }

    public void accessory_panel(int value)
    {
        left = 2;
        add_outline(left, value, accessory_button);
    }

    private void add_outline(int left, int top, List<GameObject> top_list)
    {
        for (int i = 0; i < left_button.Count; i++) {
            if (left_button[i].GetComponent<Outline>())
                Destroy(left_button[i].GetComponent<Outline>());
        }

        left_button[left].AddComponent<Outline>();
        Outline ol_1 = left_button[left].GetComponent<Outline>();
        ol_1.effectDistance = new Vector2(2, -2);

        for (int i = 0; i < top_list.Count; i++) {
            if (top_list[i].GetComponent<Outline>())
                Destroy(top_list[i].GetComponent<Outline>());
        }

        top_list[top].AddComponent<Outline>();
    }
}
