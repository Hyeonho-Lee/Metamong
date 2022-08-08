using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class RC_DB_Value
{
    public int index;
    public string name_en;
    public string name_kr;
    public string position_en;
    public string position_ko;
    public int price;
}


[Serializable]
public class RC_DB
{
    public List<RC_DB_Value> Chair01;
    public List<RC_DB_Value> Chair02;
    public List<RC_DB_Value> Ground;
    public List<RC_DB_Value> Ground_Accessory01;
    public List<RC_DB_Value> Ground_Accessory02;
    public List<RC_DB_Value> Table;
    public List<RC_DB_Value> Table_Accessory01;
    public List<RC_DB_Value> Wall01;
    public List<RC_DB_Value> Wall02;
    public List<RC_DB_Value> Wall_Accessory01;
    public List<RC_DB_Value> Wall_Accessory02;
}
