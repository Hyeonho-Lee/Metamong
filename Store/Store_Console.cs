using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Store_Console : MonoBehaviour
{
    public GameObject StorePanel;
    public GameObject FacePanel;
    public GameObject ClothesPanel;
    public GameObject AccessoryPanel;

    public bool store_trigger;

    private StoreManager sm;

    private void Awake()
    {
        sm = this.GetComponent<StoreManager>();
    }

    private void Start()
    {
        if (StorePanel.active) {
            store_trigger = false;
            StorePanel.SetActive(false);
        }
    }


    private void Update()
    {
        Input_key();
    }

    #region »óÁ¡ ¿ÀÇÂ, ´Ý±â
    public void Input_key()
    {
        if (Input.GetKeyDown(KeyCode.F)) {
            if (!store_trigger) {
                StorePanel.SetActive(true);
                store_trigger = true;

                sm.input_value = null;

                FacePanel.SetActive(false);
                ClothesPanel.SetActive(false);
                AccessoryPanel.SetActive(false);

                sm.Delete_Button();
            } else {
                StorePanel.SetActive(false);
                store_trigger = false;

                sm.input_value = null;

                FacePanel.SetActive(false);
                ClothesPanel.SetActive(false);
                AccessoryPanel.SetActive(false);
            }
        }
    }

    public void Store_Exit()
    {
        if (!store_trigger) {
            StorePanel.SetActive(true);
            store_trigger = true;

            sm.Delete_Button();
        } else {
            StorePanel.SetActive(false);
            store_trigger = false;
        }

        sm.input_value = null;

        FacePanel.SetActive(false);
        ClothesPanel.SetActive(false);
        AccessoryPanel.SetActive(false);
    }
    #endregion

    #region ¿ÞÂÊ Ä«Å×°í¸®
    public void Click_Face()
    {
        FacePanel.SetActive(true);
        ClothesPanel.SetActive(false);
        AccessoryPanel.SetActive(false);

        sm.Delete_Button();
    }

    public void Click_Clothes()
    {
        FacePanel.SetActive(false);
        ClothesPanel.SetActive(true);
        AccessoryPanel.SetActive(false);

        sm.Delete_Button();
    }

    public void Click_Aceessory()
    {
        FacePanel.SetActive(false);
        ClothesPanel.SetActive(false);
        AccessoryPanel.SetActive(true);

        sm.Delete_Button();
    }
    #endregion

    #region »ó´Ü Ä«Å×°í¸®
    public void Input_Value(string name)
    {
        sm.input_value = name;
        sm.Delete_Button();
        sm.Create_Bnt();
    }
    #endregion
}
