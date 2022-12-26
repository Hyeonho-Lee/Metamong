using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Proyecto26;
using System;
using FullSerializer;
using UnityEngine.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public class Auth_Controller : MonoBehaviour
{ 
    public static fsSerializer serializer = new fsSerializer();

    private string database_url = "https://metamong-16b1b-default-rtdb.firebaseio.com";
    private string signup_url = "https://www.googleapis.com/identitytoolkit/v3/relyingparty/signupNewUser?key=";
    private string signin_url = "https://www.googleapis.com/identitytoolkit/v3/relyingparty/verifyPassword?key=";
    private string auth_key = "AIzaSyD5rNq2oe20orEMCNwO06qePZn2BNYTyw8";

    [Header("User Info")]
    public string userName;
    public string idToken;
    public string localId;
    public string error_code;
    public string world_position;
    public string room_index;

    [Header("Data Info")]
    public User user = new User();
    public CC_User cc_user = new CC_User();
    public CC_DB cc_db = new CC_DB();
    public RC_User rc_user = new RC_User();
    public RC_DB rc_db = new RC_DB();
    public RC_Info rc_info = new RC_Info();
    public House house = new House();

    [Header("All_House Info")]
    public List<string> h_uid = new List<string>();
    public List<string> h_username = new List<string>();
    public List<int> h_position_index = new List<int>();
    public List<int> h_house_index = new List<int>();
    public List<bool> h_is_house = new List<bool>();

    #region 회원가입

    public void SignUp_User(string username, string email, string password, bool is_user, bool is_counselor)
    {
        string userData = "{\"email\":\"" + email + "\",\"password\":\"" + password + "\",\"returnSecureToken\":true}";
        RestClient.Post<SignResponse>(signup_url + auth_key, userData).Then(
            response =>
            {
                userName = username;
                idToken = response.idToken;
                localId = response.localId;

                user.username = username;
                user.email = email;
                user.password = password;
                user.uid = response.localId;
                user.is_user = is_user;
                user.is_counselor = is_counselor;
                user.is_counselor_check = false;
                user.money = 3000;

                //Debug.Log("회원가입 성공");
                PostToDatabase("user");

                error_code = "";
            }).Catch(error =>
            {
                error_code = error.Message;
                switch (error_code) {
                    case "HTTP/1.1 400 Bad Request":
                        error_code = "400";
                        break;
                }
                //Debug.Log(error);
            });
    }

    #endregion

    #region 로그인

    public void SignIn_User(string email, string password)
    {
        string userData = "{\"email\":\"" + email + "\",\"password\":\"" + password + "\",\"returnSecureToken\":true}";
        RestClient.Post<SignResponse>(signin_url + auth_key, userData).Then(
            response =>
            {
                idToken = response.idToken;
                localId = response.localId;

                //Debug.Log("로그인 성공");
                GetToDatabase("user");

                error_code = "";
            }).Catch(error =>
            {
                error_code = error.Message;
                switch (error_code) {
                    case "HTTP/1.1 400 Bad Request":
                        error_code = "400";
                        break;
                }
                //Debug.Log(error);
            });
    }

    #endregion

    #region Uid 가져오기

    // 로그인이 완료후 발급된 Token을 통해 uid 또는 다른 데이터 접근이 가능하다.
    private void GetLocalId()
    {
        /*RestClient.Get(database_url + "/user/" + ".json?auth=" + idToken).Then(response =>
        {
            string username = uid_text.text;

            fsData userData = fsJsonParser.Parse(response.Text);
            Dictionary<string, User> users = null;
            serializer.TryDeserialize(userData, ref users);

            foreach (var user in users.Values)
            {
                if (user.username == username)
                {
                    localId = user.uid;
                    RetrieveFromDatabase();
                    break;
                }
            }
        }).Catch(error =>
        {
            Debug.Log(error);
        });*/
    }

    private void RetrieveFromDatabase()
    {
        RestClient.Get<User>(database_url + "/user/" + localId + ".json?auth=" + idToken).Then(response =>
        {
            user = response;
            print(user.uid);
        });
    }

    #endregion

    #region 파이어베이스 접근
    // 데이터 업로드
    private void PostToDatabase(string value)
    {
        switch (value)
        {
            case "user":
                RestClient.Put(database_url + "/user/" + localId + ".json?auth=" + idToken, user);
                break;
            case "character_custom":
                cc_user.username = userName;
                cc_user.uid = localId;
                RestClient.Put(database_url + "/character/" + localId + ".json?auth=" + idToken, cc_user);
                break;
            case "room_custom":
                rc_user.username = userName;
                rc_user.uid = localId;
                RestClient.Put(database_url + "/room/" + localId + ".json?auth=" + idToken, rc_user);
                break;
            case "house":
                house.username = userName;
                house.uid = localId;
                RestClient.Put(database_url + "/house/" + localId + ".json?auth=" + idToken, house);
                break;
            case "user_info":
                user.username = userName;
                user.uid = localId;
                RestClient.Put(database_url + "/user/" + localId + ".json?auth=" + idToken, user);
                break;
            case "room_info":
                user.username = userName;
                user.uid = localId;
                RestClient.Put(database_url + "/room_info.json?auth=" + idToken, rc_info);
                break;
        }
    }

    // 데이터 가져오기
    private void GetToDatabase(string value)
    {
        switch (value)
        {
            case "user":
                RestClient.Get<User>(database_url + "/user/" + localId + ".json?auth=" + idToken).Then(response =>
                {
                    userName = response.username;
                });
                break;
            case "character_custom":
                RestClient.Get<CC_User>(database_url + "/character/" + localId + ".json?auth=" + idToken).Then(response =>
                {
                    cc_user.accessory01 = response.accessory01;
                    cc_user.accessory02 = response.accessory02;
                    cc_user.beard = response.beard;
                    cc_user.eye = response.eye;
                    cc_user.eyebrow = response.eyebrow;
                    cc_user.gloves = response.gloves;
                    cc_user.hair = response.hair;
                    cc_user.helmet = response.helmet;
                    cc_user.mouth = response.mouth;
                    cc_user.pants = response.pants;
                    cc_user.shoes = response.shoes;
                    cc_user.top = response.top;
                    cc_user.username = response.username;
                    cc_user.uid = response.uid;
                });
                break;
            case "character_db":
                RestClient.Get<CC_DB>(database_url + "/character_db.json").Then(response => {

                    //print(response.accessory01[0]);

                    for (int i = 0; i < response.accessory01.Count; i++) {
                        cc_db.accessory01.Add(response.accessory01[i]);
                    }

                    for (int i = 0; i < response.accessory02.Count; i++) {
                        cc_db.accessory02.Add(response.accessory02[i]);
                    }

                    for (int i = 0; i < response.beard.Count; i++) {
                        cc_db.beard.Add(response.beard[i]);
                    }

                    for (int i = 0; i < response.eye.Count; i++) {
                        cc_db.eye.Add(response.eye[i]);
                    }

                    for (int i = 0; i < response.eyebrow.Count; i++) {
                        cc_db.eyebrow.Add(response.eyebrow[i]);
                    }

                    for (int i = 0; i < response.gloves.Count; i++) {
                        cc_db.gloves.Add(response.gloves[i]);
                    }

                    for (int i = 0; i < response.hair.Count; i++) {
                        cc_db.hair.Add(response.hair[i]);
                    }

                    for (int i = 0; i < response.head.Count; i++) {
                        cc_db.head.Add(response.head[i]);
                    }

                    for (int i = 0; i < response.helmet.Count; i++) {
                        cc_db.helmet.Add(response.helmet[i]);
                    }

                    for (int i = 0; i < response.mouth.Count; i++) {
                        cc_db.mouth.Add(response.mouth[i]);
                    }

                    for (int i = 0; i < response.pants.Count; i++) {
                        cc_db.pants.Add(response.pants[i]);
                    }

                    for (int i = 0; i < response.shoes.Count; i++) {
                        cc_db.shoes.Add(response.shoes[i]);
                    }

                    for (int i = 0; i < response.top.Count; i++) {
                        cc_db.top.Add(response.top[i]);
                    }
                });
                break;
            case "room_custom":
                RestClient.Get<RC_User>(database_url + "/room/" + localId + ".json?auth=" + idToken).Then(response => {
                    rc_user.wall01 = response.wall01;
                    rc_user.wall_accessory01 = response.wall_accessory01;
                    rc_user.ground_accessory01 = response.ground_accessory01;

                    rc_user.wall02 = response.wall02;
                    rc_user.wall_accessory02 = response.wall_accessory02;
                    rc_user.ground_accessory02 = response.ground_accessory02;

                    rc_user.ground = response.ground;
                    rc_user.chair01 = response.chair01;
                    rc_user.chair02 = response.chair02;
                    rc_user.table = response.table;
                    rc_user.table_accessory01 = response.table_accessory01;

                    rc_user.username = response.username;
                    rc_user.uid = response.uid;
                });
                break;
            case "room_db":
                RestClient.Get<RC_DB>(database_url + "/room_db.json").Then(response => {

                    for (int i = 0; i < response.Chair01.Count; i++) {
                        rc_db.Chair01.Add(response.Chair01[i]);
                    }

                    for (int i = 0; i < response.Chair02.Count; i++) {
                        rc_db.Chair02.Add(response.Chair02[i]);
                    }

                    for (int i = 0; i < response.Ground.Count; i++) {
                        rc_db.Ground.Add(response.Ground[i]);
                    }

                    for (int i = 0; i < response.Ground_Accessory01.Count; i++) {
                        rc_db.Ground_Accessory01.Add(response.Ground_Accessory01[i]);
                    }

                    for (int i = 0; i < response.Ground_Accessory02.Count; i++) {
                        rc_db.Ground_Accessory02.Add(response.Ground_Accessory02[i]);
                    }

                    for (int i = 0; i < response.Table.Count; i++) {
                        rc_db.Table.Add(response.Table[i]);
                    }

                    for (int i = 0; i < response.Table_Accessory01.Count; i++) {
                        rc_db.Table_Accessory01.Add(response.Table_Accessory01[i]);
                    }

                    for (int i = 0; i < response.Wall01.Count; i++) {
                        rc_db.Wall01.Add(response.Wall01[i]);
                    }

                    for (int i = 0; i < response.Wall02.Count; i++) {
                        rc_db.Wall02.Add(response.Wall02[i]);
                    }

                    for (int i = 0; i < response.Wall_Accessory01.Count; i++) {
                        rc_db.Wall_Accessory01.Add(response.Wall_Accessory01[i]);
                    }

                    for (int i = 0; i < response.Wall_Accessory02.Count; i++) {
                        rc_db.Wall_Accessory02.Add(response.Wall_Accessory02[i]);
                    }
                });
                break;
            case "user_info":
                RestClient.Get<User>(database_url + "/user/" + localId + ".json?auth=" + idToken).Then(response => {
                    user.email = response.email;
                    user.password = response.password;
                    user.uid = response.uid;
                    user.username = response.username;
                    user.money = response.money;
                    user.is_user = response.is_user;
                    user.is_counselor = response.is_counselor;
                    user.is_counselor_check = response.is_counselor_check;
                    user.is_admin = response.is_admin;
                });
                break;
            case "room_info":
                RestClient.Get<RC_Info>(database_url + "/room_info.json").Then(response => {
                    for (int i = 0; i < response.RC_Infos.Count; i++) {
                        rc_info.RC_Infos.Add(response.RC_Infos[i]);
                    }
                });
                break;
            case "house":
                RestClient.Get<House>(database_url + "/house/" + localId + ".json?auth=" + idToken).Then(response => {
                    house.username = response.username;
                    house.uid = response.uid;
                    house.is_house = response.is_house;
                    house.position_index = response.position_index;
                    house.house_index = response.house_index;
                    house.house_date = response.house_date;
                    house.house_price = response.house_price;
                });
                break;
            case "all_house":
                RestClient.Get(database_url + "/house.json").Then(response => {
                    string json = response.Text;
                    JObject jObject = JObject.Parse(json);

                    h_uid.Clear();
                    h_username.Clear();
                    h_position_index.Clear();
                    h_house_index.Clear();
                    h_is_house.Clear();

                    foreach (JProperty info in jObject.Properties()) {
                        h_uid.Add(jObject[info.Name]["uid"].ToString());
                        h_username.Add(jObject[info.Name]["username"].ToString());
                        h_position_index.Add(int.Parse(jObject[info.Name]["position_index"].ToString()));
                        h_house_index.Add(int.Parse(jObject[info.Name]["house_index"].ToString()));
                        h_is_house.Add(bool.Parse(jObject[info.Name]["is_house"].ToString()));
                    }
                });
                break;
        }
    }

    #endregion
    //------------------- 유저 ---------------------//

    public void Update_User_Info()
    {
        PostToDatabase("user_info");
    }

    public void Get_User_Info()
    {
        GetToDatabase("user_info");
    }

    //----------------- 캐릭터 -------------------//

    public void Update_Character_Button()
    {
        PostToDatabase("character_custom");
    }

    public void Get_Character_Button()
    {
        GetToDatabase("character_custom");
    }

    public void Get_Character_DB()
    {
        GetToDatabase("character_db");
    }

    //------------------- 상담소 꾸미기 --------------------//

    public void Update_Room_Custom()
    {
        PostToDatabase("room_custom");
    }

    public void Get_Room_Custom()
    {
        GetToDatabase("room_custom");
    }

    public void Get_Room_DB()
    {
        GetToDatabase("room_db");
    }

    //------------------- 부동산 -------------------//

    public void Update_House()
    {
        PostToDatabase("house");
    }

    public void Get_House()
    {
        GetToDatabase("house");
    }

    public void Get_All_House()
    {
        GetToDatabase("all_house");
    }

    //------------------- 상담소 꾸미기 --------------------//

    public void Update_Room_Info()
    {
        PostToDatabase("room_info");
    }

    public void Get_Room_Info()
    {
        GetToDatabase("room_info");
    }
}
