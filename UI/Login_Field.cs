using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Text.RegularExpressions;

public class Login_Field : MonoBehaviour
{
    [Header("Button")]
    public GameObject enter_world;
    public GameObject res_text;

    [Header("Login_field")]
    public Text error_text;
    public InputField email_field;
    public InputField password_field;

    [Header("Res_field")]
    public Text error_texts;
    public InputField username_fields;
    public InputField email_fields;
    public InputField password_fields;
    public InputField password_check_fields;
    public Toggle user_toggle;
    public Toggle counselor_toggle;

    private GameObject console;
    private Auth_Controller ac;

    private void Start()
    {
        console = GameObject.Find("Title_Console").gameObject;
        ac = console.GetComponent<Auth_Controller>();

        error_text.text = "<color=#323232>로그인</color>";
        error_texts.text = "<color=#323232>회원가입</color>";
    }

    public void InputField_Clear(InputField field)
    {
        field.text = "";
    }

    public void Check_Login()
    {
        if (email_field.text == "" && password_field.text == "") {
            error_text.text = "<color=#FF9983>로그인 정보가 올바르지 않습니다.</color>";
        } else if (email_field.text == "" && password_field.text != "") {
            error_text.text = "<color=#FF9983>이메일을 입력해 주세요.</color>";
        } else if (email_field.text != "" && password_field.text == "") {
            error_text.text = "<color=#FF9983>비밀번호를 입력해 주세요.</color>";
        } else {
            // 둘다 입력했을시
            if (!new Regex("^[a-zA-Z0-9+_.-]+@[a-zA-Z0-9+_.-]+.[a-z]").IsMatch(email_field.text)) {
                error_text.text = "<color=#FF9983>이메일 형식이 다릅니다.</color>";
            }else if (!new Regex("^.{6,}$").IsMatch(password_field.text)) {
                error_text.text = "<color=#FF9983>비밀번호 입력이 잘못되었습니다. (최소 6글자)</color>";
            }else {
                ac.SignIn_User(email_field.text, password_field.text);
                StartCoroutine(Delay_Login());
            }
        }
    }

    IEnumerator Delay_Login()
    {
        float delay = 0.7f;

        GameObject submit = GameObject.Find("Submit").gameObject;
        submit.SetActive(false);

        error_text.text = "<color=#323232>로그인중 입니다.</color>";

        yield return new WaitForSeconds(delay);

        error_text.text = "<color=#323232>로그인중 입니다..</color>";

        yield return new WaitForSeconds(delay);

        error_text.text = "<color=#323232>로그인중 입니다...</color>";

        yield return new WaitForSeconds(delay);

        if (ac.error_code != "400") {
            error_text.text = ac.userName.ToString() + "<color=#323232>님 환영합니다!</color>";
            enter_world.SetActive(true);
            res_text.SetActive(false);
        } else {
            error_text.text = "<color=#FF9983>로그인 정보가 올바르지 않습니다.</color>";
            submit.SetActive(true);
        }
    }

    public void Check_Res()
    {
        if (username_fields.text == "" && email_fields.text == "" && password_fields.text == "" ||
            username_fields.text != "" && email_fields.text == "" && password_fields.text == "" ||
            username_fields.text == "" && email_fields.text != "" && password_fields.text == "" ||
            username_fields.text == "" && email_fields.text == "" && password_fields.text != "") {
            error_texts.text = "<color=#FF9983>입력 정보가 올바르지 않습니다.</color>";
        } else if (username_fields.text == "" && email_fields.text != "" && password_fields.text != "") {
            error_texts.text = "<color=#FF9983>닉네임을 입력해 주세요.</color>";
        } else if (username_fields.text != "" && email_fields.text == "" && password_fields.text != "") {
            error_texts.text = "<color=#FF9983>이메일을 입력해 주세요.</color>";
        } else if (username_fields.text != "" && email_fields.text != "" && password_fields.text == "") {
            error_texts.text = "<color=#FF9983>비밀번호를 입력해 주세요.</color>";
        } else {
            // 셋다 입력했을시
            if (!new Regex("^[a-zA-Z0-9+_.-]+@[a-zA-Z0-9+_.-]+.[a-z]").IsMatch(email_fields.text)) {
                error_texts.text = "<color=#FF9983>이메일 형식이 다릅니다.</color>";
            } else if (!new Regex("^.{6,}$").IsMatch(password_fields.text)) {
                error_texts.text = "<color=#FF9983>비밀번호 입력이 잘못되었습니다. (최소 6글자)</color>";
            } else if (password_fields.text != password_check_fields.text) {
                error_texts.text = "<color=#FF9983>비밀번호가 일치하지 않습니다.</color>";
            } else if (user_toggle.isOn == true && counselor_toggle.isOn == true ||
                user_toggle.isOn == false && counselor_toggle.isOn == false) {
                error_texts.text = "<color=#FF9983>사용자 설정을 확인하십시요.</color>";
            } else {
                ac.SignUp_User(username_fields.text, email_fields.text, password_fields.text, user_toggle.isOn, counselor_toggle.isOn);
                StartCoroutine(Delay_Res());
            }
        }
    }

    IEnumerator Delay_Res()
    {
        float delay = 0.5f;

        GameObject submit = GameObject.Find("Res_Submit").gameObject;
        submit.SetActive(false);

        error_texts.text = "<color=#323232>회원가입중 입니다.</color>";

        yield return new WaitForSeconds(delay);

        error_texts.text = "<color=#323232>회원가입중 입니다..</color>";

        yield return new WaitForSeconds(delay);

        error_texts.text = "<color=#323232>회원가입중 입니다...</color>";

        yield return new WaitForSeconds(delay);

        if (ac.error_code != "400") {
            error_texts.text = "<color=#323232>회원가입을 성공하였습니다!</color>";
        } else {
            error_texts.text = "<color=#FF9983>회원 정보가 올바르지 않습니다.</color>";
        }

        submit.SetActive(true);
    }

    public void Change_Scene(string name)
    {
        DontDestroyOnLoad(console);
        SceneManager.LoadScene(name);
    }

    public void admin_access()
    {
        email_field.text = "admin1@gmail.com";
        password_field.text = "12345678";
    }
}
