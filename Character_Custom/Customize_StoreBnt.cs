using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

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

        item_moudle.Item_accessory01 = Accessory01sprites.OfType<Sprite>().ToList();
        item_moudle.Item_accessory02 = Accessory02sprites.OfType<Sprite>().ToList();
        item_moudle.Item_beard = Beardsprites.OfType<Sprite>().ToList();
        item_moudle.Item_eye = Eyesprites.OfType<Sprite>().ToList();
        item_moudle.Item_eyebrow = Eyebrowsprites.OfType<Sprite>().ToList();
        item_moudle.Item_gloves = Glovessprites.OfType<Sprite>().ToList();
        item_moudle.Item_hair = Hairsprites.OfType<Sprite>().ToList();
        item_moudle.Item_head = Headsprites.OfType<Sprite>().ToList();
        item_moudle.Item_helmet = Helmetsprites.OfType<Sprite>().ToList();
        item_moudle.Item_mouth = Mouthsprites.OfType<Sprite>().ToList();
        item_moudle.Item_pants = Pantssprites.OfType<Sprite>().ToList();
        item_moudle.Item_shoes = Shoessprites.OfType<Sprite>().ToList();
        item_moudle.Item_top = Topsprites.OfType<Sprite>().ToList();
    }
}