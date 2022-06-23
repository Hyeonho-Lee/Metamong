using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class DB_Value
{
    public string category_en;
    public string category_ko;
    public string division_en;
    public string division_ko;
    public string file_name;
    public string folder_name;
    public int index;
    public string name_en;
    public string name_kr;
    public string path;
    public int price;
}


[Serializable]
public class CC_DB
{
    public List<DB_Value> accessory01;
    public List<DB_Value> accessory02;
    public List<DB_Value> beard;
    public List<DB_Value> eye;
    public List<DB_Value> eyebrow;
    public List<DB_Value> gloves;
    public List<DB_Value> hair;
    public List<DB_Value> head;
    public List<DB_Value> helmet;
    public List<DB_Value> mouth;
    public List<DB_Value> pants;
    public List<DB_Value> shoes;
    public List<DB_Value> top;
}
