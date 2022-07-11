using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Item_Moudle
{
    public List<Sprite> Item_eyebrow; // 눈썹
    public List<Sprite> Item_eye; // 눈
    public List<Sprite> Item_beard; // 수염
    public List<Sprite> Item_mouth; // 입
    public List<Sprite> Item_hair; // 머리카락
    public List<Sprite> Item_head; //코 모양

    public List<Sprite> Item_top; // 상의
    public List<Sprite> Item_pants; // 하의
    public List<Sprite> Item_shoes; // 신발
    public List<Sprite> Item_gloves; //손

    public List<Sprite> Item_accessory01; // 
    public List<Sprite> Item_accessory02; // 
    public List<Sprite> Item_helmet; //모자
    public List<Sprite> Item_weapon; // 무기

    public void Item_Reset_Input()
    {
        Item_eyebrow = new List<Sprite>();
        Item_eye = new List<Sprite>();
        Item_beard = new List<Sprite>();
        Item_mouth = new List<Sprite>();
        Item_hair = new List<Sprite>();
        Item_head = new List<Sprite>();

        Item_top = new List<Sprite>();
        Item_pants = new List<Sprite>();
        Item_shoes = new List<Sprite>();
        Item_gloves = new List<Sprite>();

        Item_accessory01 = new List<Sprite>();
        Item_accessory02 = new List<Sprite>();
        Item_helmet = new List<Sprite>();
        Item_weapon = new List<Sprite>();
    }
}

public class Customize_StoreBnt : MonoBehaviour
{
    public Item_Moudle item_moudle = new Item_Moudle(); //아이템 개체 생성

    private void Start()
    {
        Check_Item_Module();
    }

    public void Check_Item_Module()
    {
        Sprite[] Accessory01sprites = Resources.LoadAll<Sprite>("item_sprite/Accessory01");
        Sprite[] Accessory02sprites = Resources.LoadAll<Sprite>("item_sprite/Accessory02");
        Sprite[] Beardsprites = Resources.LoadAll<Sprite>("item_sprite/Beard");
        Sprite[] Eyesprites = Resources.LoadAll<Sprite>("item_sprite/Eye");
        Sprite[] Eyebrowsprites = Resources.LoadAll<Sprite>("item_sprite/Eyebrow");
        Sprite[] Glovessprites = Resources.LoadAll<Sprite>("item_sprite/Gloves");
        Sprite[] Hairsprites = Resources.LoadAll<Sprite>("item_sprite/Hair");
        Sprite[] Headsprites = Resources.LoadAll<Sprite>("item_sprite/Head");
        Sprite[] Helmetsprites = Resources.LoadAll<Sprite>("item_sprite/Helmet");
        Sprite[] Mouthsprites = Resources.LoadAll<Sprite>("item_sprite/Mouth");
        Sprite[] Pantssprites = Resources.LoadAll<Sprite>("item_sprite/Pants");
        Sprite[] Shoessprites = Resources.LoadAll<Sprite>("item_sprite/Shoes");
        Sprite[] Topsprites = Resources.LoadAll<Sprite>("item_sprite/Top");


        for (int i = 0; i < Accessory01sprites.Length; i++) item_moudle.Item_accessory01.Add(Accessory01sprites[i]);

        for (int i = 0; i < Accessory02sprites.Length; i++) item_moudle.Item_accessory02.Add(Accessory02sprites[i]);

        for (int i = 0; i < Beardsprites.Length; i++) item_moudle.Item_beard.Add(Beardsprites[i]);

        for (int i = 0; i < Eyesprites.Length; i++) item_moudle.Item_eye.Add(Eyesprites[i]);

        for (int i = 0; i < Eyebrowsprites.Length; i++) item_moudle.Item_eyebrow.Add(Eyebrowsprites[i]);

        for (int i = 0; i < Glovessprites.Length; i++) item_moudle.Item_gloves.Add(Glovessprites[i]);

        for (int i = 0; i < Hairsprites.Length; i++) item_moudle.Item_hair.Add(Hairsprites[i]);

        for (int i = 0; i < Headsprites.Length; i++) item_moudle.Item_head.Add(Headsprites[i]);

        for (int i = 0; i < Helmetsprites.Length; i++) item_moudle.Item_helmet.Add(Helmetsprites[i]);

        for (int i = 0; i < Mouthsprites.Length; i++) item_moudle.Item_mouth.Add(Mouthsprites[i]);

        for (int i = 0; i < Pantssprites.Length; i++) item_moudle.Item_pants.Add(Pantssprites[i]);

        for (int i = 0; i < Shoessprites.Length; i++) item_moudle.Item_shoes.Add(Shoessprites[i]);

        for (int i = 0; i < Topsprites.Length; i++) item_moudle.Item_top.Add(Topsprites[i]);

    }
}