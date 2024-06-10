using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoginData : MonoBehaviour
{
    public PlayerData playerdata = new PlayerData();
    protected string m_playerID;
    protected string m_playerPW;
    protected string m_playerName;

    [SerializeField] protected TMP_InputField createID;
    [SerializeField] protected TMP_InputField createPW;
    [SerializeField] protected TMP_InputField checkPW;
    [SerializeField] protected TMP_InputField createname;
    [SerializeField] protected TMP_InputField login_ID;
    [SerializeField] protected TMP_InputField login_PW;

}
