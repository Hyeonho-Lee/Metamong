using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class User
{
    public string username;
    public string email;
    public string password;
    public string uid;

    public User()
    {
        username = Auth_Controller.username_value;
        email = Auth_Controller.email_value;
        password = Auth_Controller.password_value;
        uid = Auth_Controller.uid_value;
    }
}
