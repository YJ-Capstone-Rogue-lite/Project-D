using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Title_Btn : MonoBehaviour
{
    [SerializeReference] private GameObject newGame;
    [SerializeReference] private GameObject loadGame;
    [SerializeReference] private GameObject option;
    [SerializeReference] private GameObject quitGame;
    [SerializeReference] private GameObject popUP;
    [SerializeReference] private GameObject option_Popup;
    [SerializeReference] private GameObject apply;
    [SerializeReference] private GameObject yes;
    [SerializeReference] private GameObject no;

    [SerializeReference] private Button Load_Btn;

    [SerializeReference] private TMP_Text popup_Text;
    

    private void Start()
    {
        if(File.Exists(Application.persistentDataPath + "/" + DataManager.Instance.GameDataFileName))
        {
            Load_Btn.GetComponent<Button>().interactable = true;
        }
        else
        {
            Load_Btn.GetComponent<Button>().interactable = false;
        }
        DataManager.Instance.LoadGameData();
        option_Popup.SetActive(false);
        popUP.SetActive(false);
    }

    private void OnApplicationQuit()
    {
        DataManager.Instance.SaveGameData();
    }

    public void Click_newGame()
    {
        if (File.Exists(Application.persistentDataPath + "/" + DataManager.Instance.GameDataFileName)) // 플레이어 데이터가 있을경우
        {
            popUP.SetActive(true);
        }
        else
        {
            // 데이터가 없을 때 씬 변경
        }
    }

    public void Click_loadGame()
    {
        // 인게임 씬으로 변경
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
