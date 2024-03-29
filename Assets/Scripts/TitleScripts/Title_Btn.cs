using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Title_Btn : MonoBehaviour
{
    [SerializeReference] private GameObject newGame_Btn;
    [SerializeReference] private GameObject loadGame_Btn;
    [SerializeReference] private GameObject option_Btn;
    [SerializeReference] private GameObject quitGame_Btn;
    [SerializeReference] private GameObject popUP;
    [SerializeReference] private GameObject option_Popup;
    [SerializeReference] private GameObject apply_Btn;
    [SerializeReference] private GameObject yes_Btn;
    [SerializeReference] private GameObject no_Btn;

    [SerializeReference] private TMP_Text popup_Text;

    private void Start()
    {
        option_Popup.SetActive(false);
        popUP.SetActive(false);
    }

    public void Click_newGame()
    {
        if (true) // �÷��̾� �����Ͱ� ������� ����
        {
            popUP.SetActive(true);
        }
        else
        {
            // �����Ͱ� ���� �� �� ����
        }
    }

    public void Click_loadGame()
    {
        // �ΰ��� ������ ����
    }

    public void Click_option()
    {
        option_Popup.SetActive(true);
    }

    public void Click_QuitGame()
    {
        popUP.SetActive(true);
        popup_Text.text = "Are you sure you want to exit the game?";
    }

    public void Close_Btn()
    {
        if(option_Popup.activeSelf == true)
        {
            option_Popup.SetActive(false);
        }
        else if(popUP.activeSelf == true)
        {
            popUP.SetActive(false);
        }
    }

    public void quit_Btn()
    {
        Application.Quit();
    }



}
