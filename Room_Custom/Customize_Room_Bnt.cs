using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

[System.Serializable]
public class Room_Moudles
{
    public List<Sprite> wall01;
    public List<Sprite> wall_accessory01;
    public List<Sprite> ground_accessory01;
    public List<Sprite> wall02;
    public List<Sprite> wall_accessory02;
    public List<Sprite> ground_accessory02;
    public List<Sprite> ground;
    public List<Sprite> chair01;
    public List<Sprite> chair02;
    public List<Sprite> table;
    public List<Sprite> table_accessory01;
}

public class Customize_Room_Bnt : MonoBehaviour
{
    public Room_Moudles room_moudles = new Room_Moudles();

    private void Start()
    {
        Check_Item_Module();
    }

    public void Check_Item_Module()
    {
        Sprite[] chair01_sprites = Resources.LoadAll<Sprite>("room_sprite/Chair01");
        Sprite[] chair02_sprites = Resources.LoadAll<Sprite>("room_sprite/Chair02");
        Sprite[] ground_sprites = Resources.LoadAll<Sprite>("room_sprite/Ground");
        Sprite[] ground_accessory01_sprites = Resources.LoadAll<Sprite>("room_sprite/Ground_Accessory01");
        Sprite[] ground_accessory02_sprites = Resources.LoadAll<Sprite>("room_sprite/Ground_Accessory02");
        Sprite[] table_sprites = Resources.LoadAll<Sprite>("room_sprite/Table");
        Sprite[] table_accessory01_sprites = Resources.LoadAll<Sprite>("room_sprite/Table_Accessory01");
        Sprite[] wall01_sprites = Resources.LoadAll<Sprite>("room_sprite/Wall01");
        Sprite[] wall02_sprites = Resources.LoadAll<Sprite>("room_sprite/Wall02");
        Sprite[] wall_accessory01_sprites = Resources.LoadAll<Sprite>("room_sprite/Wall_Accessory01");
        Sprite[] wall_accessory02_sprites = Resources.LoadAll<Sprite>("room_sprite/Wall_Accessory02");

        room_moudles.chair01 = chair01_sprites.OfType<Sprite>().ToList();
        room_moudles.chair02 = chair02_sprites.OfType<Sprite>().ToList();
        room_moudles.ground = ground_sprites.OfType<Sprite>().ToList();
        room_moudles.ground_accessory01 = ground_accessory01_sprites.OfType<Sprite>().ToList();
        room_moudles.ground_accessory02 = ground_accessory02_sprites.OfType<Sprite>().ToList();
        room_moudles.table = table_sprites.OfType<Sprite>().ToList();
        room_moudles.table_accessory01 = table_accessory01_sprites.OfType<Sprite>().ToList();
        room_moudles.wall01 = wall01_sprites.OfType<Sprite>().ToList();
        room_moudles.wall02 = wall02_sprites.OfType<Sprite>().ToList();
        room_moudles.wall_accessory01 = wall_accessory01_sprites.OfType<Sprite>().ToList();
        room_moudles.wall_accessory02 = wall_accessory02_sprites.OfType<Sprite>().ToList();
    }
}