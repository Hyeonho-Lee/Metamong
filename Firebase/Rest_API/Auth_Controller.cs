using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Proyecto26;
using System;
using FullSerializer;
using UnityEngine.Serialization;
using UnityEngine.SceneManagement;

public class Auth_Controller : MonoBehaviour
{
    public static string username_value;
    public static string email_value;
    public static string password_value;
    public static string uid_value;

    public static fsSerializer serializer = new fsSerializer();

    private string database_url = "https://metamong-16b1b-default-rtdb.firebaseio.com";
    private string signup_url = "https://www.googleapis.com/identitytoolkit/v3/relyingparty/signupNewUser?key=";
    private string signin_url = "https://www.googleapis.com/identitytoolkit/v3/relyingparty/verifyPassword?key=";
    private string auth_key = "AIzaSyD5rNq2oe20orEMCNwO06qePZn2BNYTyw8";

    public string userName;
    public string idToken;
    public string localId;

    private GameObject console;

    private Text uid_text;
    private InputField username_text;
    private InputField email_text;
    private InputField password_text;

    User user = new User();
    public CC_User cc_user = new CC_User();
    public CC_DB cc_db = new CC_DB();

    private void Start()
    {
        console = GameObject.Find("Title_Console").gameObject;
        uid_text = GameObject.Find("Uid_Text").GetComponent<Text>();
        username_text = GameObject.Find("Username_Field").GetComponent<InputField>();
        email_text = GameObject.Find("Email_Field").GetComponent<InputField>();
        password_text = GameObject.Find("Password_Field").GetComponent<InputField>();
    }

    #region È¸¿ø°¡ÀÔ
    public void SignUp_Button()
    {
        userName = "";
        idToken = "";
        localId = "";

        SignUp_User(username_text.text, email_text.text, password_text.text);
    }

    private void SignUp_User(string username, string email, string password)
    {
        string userData = "{\"email\":\"" + email + "\",\"password\":\"" + password + "\",\"returnSecureToken\":true}";
        RestClient.Post<SignResponse>(signup_url + auth_key, userData).Then(
            response =>
            {
                idToken = response.idToken;
                localId = response.localId;
                username_value = username;
                email_value = email;
                password_value = password;
                uid_value = response.localId;
                Debug.Log("È¸¿ø°¡ÀÔ ¼º°ø");
                PostToDatabase("user");
            }).Catch(error =>
            {
                Debug.Log(error);
            });
    }

    #endregion

    #region ·Î±×ÀÎ
    public void SignIn_Button()
    {
        userName = "";
        idToken = "";
        localId = "";

        SignIn_User(email_text.text, password_text.text);
    }

    private void SignIn_User(string email, string password)
    {
        string userData = "{\"email\":\"" + email + "\",\"password\":\"" + password + "\",\"returnSecureToken\":true}";
        RestClient.Post<SignResponse>(signin_url + auth_key, userData).Then(
            response =>
            {
                idToken = response.idToken;
                localId = response.localId;
                Debug.Log("·Î±×ÀÎ ¼º°ø");
                GetToDatabase("user");
            }).Catch(error =>
            {
                Debug.Log(error);
            });
    }

    #endregion

    #region Uid °¡Á®¿À±â

    // ·Î±×ÀÎÀÌ ¿Ï·áÈÄ ¹ß±ÞµÈ TokenÀ» ÅëÇØ uid ¶Ç´Â ´Ù¸¥ µ¥ÀÌÅÍ Á¢±ÙÀÌ °¡´ÉÇÏ´Ù.
    private void GetLocalId()
    {
        RestClient.Get(database_url + "/user/" + ".json?auth=" + idToken).Then(response =>
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
        });
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

    #region ÆÄÀÌ¾îº£ÀÌ½º Á¢±Ù
    // µ¥ÀÌÅÍ ¾÷·Îµå
    private void PostToDatabase(string value)
    {
        switch (value)
        {
            case "user":
                User user = new User();
                RestClient.Put(database_url + "/user/" + localId + ".json?auth=" + idToken, user);
                break;
            case "character_custom":
                cc_user.username = userName;
                cc_user.uid = localId;
                RestClient.Put(database_url + "/character/" + localId + ".json?auth=" + idToken, cc_user);
                Debug.Log("¾Æ¹ÙÅ¸ ¾÷·Îµå ¿Ï·á");
                break;
        }
    }

    // µ¥ÀÌÅÍ °¡Á®¿À±â
    private void GetToDatabase(string value)
    {
        switch (value)
        {
            case "user":
                RestClient.Get<User>(database_url + "/user/" + localId + ".json?auth=" + idToken).Then(response =>
                {
                    uid_text.text = response.username;
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
                    Debug.Log("¾Æ¹ÙÅ¸ °¡Á®¿À±â ¿Ï·á");
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

                    Debug.Log("Ä³¸¯ÅÍ µ¥ÀÌÅÍ ºÒ·¯¿À±â ¿Ï·á");
                });
                break;
        }
    }

    #endregion

    #region Scene ÀüÈ¯
    public void Change_Scene(string name)
    {
        DontDestroyOnLoad(console);
        //SceneManager.LoadScene("Character_Custom");
        SceneManager.LoadScene(name);
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

    public void Get_Character_DB()
    {
        GetToDatabase("character_db");
    }
}
