using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Title_Btn : MonoBehaviour
{
    [SerializeField] private GameObject newGame;
    [SerializeField] private GameObject loadGame;
    [SerializeField] private GameObject option;
    [SerializeField] private GameObject quitGame;
    [SerializeField] private GameObject popUP;
    [SerializeField] private GameObject option_Popup;
    [SerializeField] private GameObject apply;
    [SerializeField] private GameObject yes;
    [SerializeField] private GameObject no;
    [SerializeField] private Image fullScreen_Box;
    [SerializeField] private Image windowScreen_Box;
    [SerializeField] private Sprite checkBox;
    [SerializeField] private Sprite emptyBox;


    [SerializeField] private Button Load_Btn;

    [SerializeField] private TMP_Text popup_Text;


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
            popup_Text.text = "Saved data is present. Do you want to clear it and proceed?";
        }
        else
        {
            play_inGame(); //없을 경우 씬 변경
        }
    }

    public void Click_loadGame()
    {
        play_inGame();// 인게임 씬으로 변경
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

    public void Yes_Btn()
    {
        if(popup_Text.text == "Are you sure you want to exit the game?")
        {
            Application.Quit();
        }
        else if(popup_Text.text == "Saved data is present. Do you want to clear it and proceed?")
        {
            play_inGame();
        }
    }

    public void quit_Btn()
    {
        Application.Quit();
    }

    public void FullScreen_Btn()
    {
        fullScreen_Box.sprite = checkBox;
        windowScreen_Box.sprite = emptyBox;

        Screen.SetResolution(1920, 1080, true);

    }
    public void WindowScreen_btn()
    {
        fullScreen_Box.sprite = emptyBox;
        windowScreen_Box.sprite = checkBox;

        Screen.SetResolution(1600, 900, false);
    }

    public void play_inGame()
    {
        SceneManager.LoadScene("In_game");
    }

}
