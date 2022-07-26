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

        error_text.text = "<color=#323232>�α���</color>";
        error_texts.text = "<color=#323232>ȸ������</color>";
    }

    public void InputField_Clear(InputField field)
    {
        field.text = "";
    }

    public void Check_Login()
    {
        if (email_field.text == "" && password_field.text == "") {
            error_text.text = "<color=#FF9983>�α��� ������ �ùٸ��� �ʽ��ϴ�.</color>";
        } else if (email_field.text == "" && password_field.text != "") {
            error_text.text = "<color=#FF9983>�̸����� �Է��� �ּ���.</color>";
        } else if (email_field.text != "" && password_field.text == "") {
            error_text.text = "<color=#FF9983>��й�ȣ�� �Է��� �ּ���.</color>";
        } else {
            // �Ѵ� �Է�������
            if (!new Regex("^[a-zA-Z0-9+_.-]+@[a-zA-Z0-9+_.-]+.[a-z]").IsMatch(email_field.text)) {
                error_text.text = "<color=#FF9983>�̸��� ������ �ٸ��ϴ�.</color>";
            }else if (!new Regex("^.{6,}$").IsMatch(password_field.text)) {
                error_text.text = "<color=#FF9983>��й�ȣ �Է��� �߸��Ǿ����ϴ�. (�ּ� 6����)</color>";
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

        error_text.text = "<color=#323232>�α����� �Դϴ�.</color>";

        yield return new WaitForSeconds(delay);

        error_text.text = "<color=#323232>�α����� �Դϴ�..</color>";

        yield return new WaitForSeconds(delay);

        error_text.text = "<color=#323232>�α����� �Դϴ�...</color>";

        yield return new WaitForSeconds(delay);

        if (ac.error_code != "400") {
            error_text.text = ac.userName.ToString() + "<color=#323232>�� ȯ���մϴ�!</color>";
            enter_world.SetActive(true);
            res_text.SetActive(false);
        } else {
            error_text.text = "<color=#FF9983>�α��� ������ �ùٸ��� �ʽ��ϴ�.</color>";
            submit.SetActive(true);
        }
    }

    public void Check_Res()
    {
        if (username_fields.text == "" && email_fields.text == "" && password_fields.text == "" ||
            username_fields.text != "" && email_fields.text == "" && password_fields.text == "" ||
            username_fields.text == "" && email_fields.text != "" && password_fields.text == "" ||
            username_fields.text == "" && email_fields.text == "" && password_fields.text != "") {
            error_texts.text = "<color=#FF9983>�Է� ������ �ùٸ��� �ʽ��ϴ�.</color>";
        } else if (username_fields.text == "" && email_fields.text != "" && password_fields.text != "") {
            error_texts.text = "<color=#FF9983>�г����� �Է��� �ּ���.</color>";
        } else if (username_fields.text != "" && email_fields.text == "" && password_fields.text != "") {
            error_texts.text = "<color=#FF9983>�̸����� �Է��� �ּ���.</color>";
        } else if (username_fields.text != "" && email_fields.text != "" && password_fields.text == "") {
            error_texts.text = "<color=#FF9983>��й�ȣ�� �Է��� �ּ���.</color>";
        } else {
            // �´� �Է�������
            if (!new Regex("^[a-zA-Z0-9+_.-]+@[a-zA-Z0-9+_.-]+.[a-z]").IsMatch(email_fields.text)) {
                error_texts.text = "<color=#FF9983>�̸��� ������ �ٸ��ϴ�.</color>";
            } else if (!new Regex("^.{6,}$").IsMatch(password_fields.text)) {
                error_texts.text = "<color=#FF9983>��й�ȣ �Է��� �߸��Ǿ����ϴ�. (�ּ� 6����)</color>";
            } else if (password_fields.text != password_check_fields.text) {
                error_texts.text = "<color=#FF9983>��й�ȣ�� ��ġ���� �ʽ��ϴ�.</color>";
            } else if (user_toggle.isOn == true && counselor_toggle.isOn == true ||
                user_toggle.isOn == false && counselor_toggle.isOn == false) {
                error_texts.text = "<color=#FF9983>����� ������ Ȯ���Ͻʽÿ�.</color>";
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

        error_texts.text = "<color=#323232>ȸ�������� �Դϴ�.</color>";

        yield return new WaitForSeconds(delay);

        error_texts.text = "<color=#323232>ȸ�������� �Դϴ�..</color>";

        yield return new WaitForSeconds(delay);

        error_texts.text = "<color=#323232>ȸ�������� �Դϴ�...</color>";

        yield return new WaitForSeconds(delay);

        if (ac.error_code != "400") {
            error_texts.text = "<color=#323232>ȸ�������� �����Ͽ����ϴ�!</color>";
        } else {
            error_texts.text = "<color=#FF9983>ȸ�� ������ �ùٸ��� �ʽ��ϴ�.</color>";
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
