using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Proyecto26;
using System;
using FullSerializer;
using UnityEngine.Serialization;

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

    [Header("Data Info")]
    public User user = new User();
    public CC_User cc_user = new CC_User();
    public CC_DB cc_db = new CC_DB();
    public RC_User rc_user = new RC_User();

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
                user.money = 100;

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
                Debug.Log("아바타 업로드 완료");
                break;
            case "room_custom":
                rc_user.username = userName;
                rc_user.uid = localId;
                RestClient.Put(database_url + "/room/" + localId + ".json?auth=" + idToken, rc_user);
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
                    Debug.Log("아바타 가져오기 완료");
                });
                break;
            case "character_db":
                RestClient.Get<CC_DB>(database_url + "/character_db.json").Then(response => {

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

                    Debug.Log("캐릭터 데이터 불러오기 완료");
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
                    //Debug.Log("아바타 가져오기 완료");
                });
                break;
        }
    }

    #endregion

    public void Update_Character_Button()
    {
        PostToDatabase("character_custom");
    }

    public void Get_Character_Button()
    {
        GetToDatabase("character_custom");
    }

    //--------------------------------------------//

    public void Get_Character_DB()
    {
        GetToDatabase("character_db");
    }

    //--------------------------------------------//

    public void Update_Room()
    {
        PostToDatabase("room_custom");
    }

    public void Get_Room()
    {
        GetToDatabase("room_custom");
    }
}
