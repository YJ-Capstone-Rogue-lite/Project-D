using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Title_Manager : LoginData
{
    [SerializeField] private GameObject newGame;
    [SerializeField] private GameObject loadGame;
    [SerializeField] private GameObject option;
    [SerializeField] private GameObject quitGame;
    [SerializeField] private GameObject popUP;
    [SerializeField] private GameObject option_Popup;
    [SerializeField] private GameObject pressBtnBG;
    [SerializeField] private GameObject gameStartBtn_BG;
    [SerializeField] private GameObject login_BG;
    [SerializeField] private GameObject sign_up_BG;
    [SerializeField] private GameObject login_Btn;
    [SerializeField] private GameObject login_popup;
    [SerializeField] private GameObject apply;
    [SerializeField] private GameObject yes;
    [SerializeField] private GameObject no;

    [SerializeField] private Image fullScreen_Box;
    [SerializeField] private Image windowScreen_Box;

    [SerializeField] private Sprite checkBox;
    [SerializeField] private Sprite emptyBox;


    [SerializeField] private Button Load_Btn;
    [SerializeField] private Button signup_TextBtn;

    [SerializeField] private TMP_Text popup_Text;
    [SerializeField] private TMP_Text login_popup_Text;


    private void Start()
    {
        //DataManager에서 파일 가져와서 playerID와 PW에 저장
        DataManager.Instance.LoadGameData();

        playerdata = DataManager.Instance.data;
        if (playerdata.player_ID != null && playerdata.player_PW != null)
        {
            m_playerID = playerdata.player_ID;
            m_playerPW = playerdata.player_PW;
        }
        else
        {
            return;
        }

        // 저장된 게임 데이터가 있으면 로드 버튼 활성화
        if (File.Exists(Application.persistentDataPath + "/" + DataManager.Instance.GameDataFileName))
        {
            Load_Btn.GetComponent<Button>().interactable = true;
        }
        else
        {
            Load_Btn.GetComponent<Button>().interactable = false;
        }


        option_Popup.SetActive(false);
        popUP.SetActive(false);
        Debug.Log(playerdata.player_ID);
        Debug.Log(playerdata.player_PW);

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

    public void Close_OPtion_Btn()
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

    public void pressBtn()
    {
        pressBtnBG.SetActive(false);
        login_BG.SetActive(true);
    }

    //로그인
    public void SignUp()
    {
        sign_up_BG.SetActive(true);
        login_BG.SetActive(false);
    }
    public void Login()
    {
        if (login_ID.text == playerdata.player_ID && login_PW.text == playerdata.player_PW)
        {
            //로그인 성공
            login_BG.SetActive(false);
            gameStartBtn_BG.SetActive(true);
        }
        else if (login_ID.text != playerdata.player_ID)
        {
            login_popup.SetActive(true);
            login_popup_Text.text = "아이디를 확인해주세요";
        }
        else if(login_PW.text != playerdata.player_PW)
        {
            login_popup.SetActive(true);
            login_popup_Text.text = "비밀번호를 확인해주세요";
        }
    }

    public void Close_LoginPopUp()
    {
        Debug.Log("눌림");
        login_popup.SetActive(false);
    }


    public void CreateAccount()
    {
        playerdata.player_ID = createID.text;
        if (createPW.text == checkPW.text)
        {
            playerdata.player_PW = createPW.text;
            sign_up_BG.SetActive(false);
            login_BG.SetActive(true);
        }
        else
        {
            login_popup.SetActive(true);
            login_popup_Text.text = "비밀번호가 같지않습니다.";
        }
        playerdata.player_name = createname.text;

    }
}
