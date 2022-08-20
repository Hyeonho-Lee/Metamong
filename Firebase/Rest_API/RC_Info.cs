using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class RC_Info_Value
{
    public string content;
    public string grade;
    public int index;
    public string info;
    public bool open;
    public string time;
    public string title;
    public string type;
    public string username;
    public string uid;
}

[Serializable]
public class RC_Info
{
    public List<RC_Info_Value> RC_Infos;
}