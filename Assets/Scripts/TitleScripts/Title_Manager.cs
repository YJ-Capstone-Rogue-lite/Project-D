using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Title_Manager : LoginData
{
    [SerializeField] private AudioClip titleMusic;
    [SerializeField] private AudioClip buttonSound;
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

    [SerializeField] private GameObject Web_Waitting;

    private void Start()
    {
        //DataManager에서 파일 가져와서 playerID와 PW에 저장
        // DataManager.Instance.LoadGameData();

        SoundManager.SetMusic(titleMusic);

        // playerdata = DataManager.Instance.data;
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

    // private void OnApplicationQuit()
    // {
    //     DataManager.Instance.SaveGameData(playerdata);
    // }

    public void Click_newGame()
    {
        SoundManager.PlaySFX(buttonSound);
        if (File.Exists(Application.persistentDataPath + "/" + DataManager.Instance.GameDataFileName)) // 플레이어 데이터가 있을경우
        {
            DataManager.Instance.SaveGameData(new PlayerData());
            popUP.SetActive(true);
            popup_Text.text = "저장된 데이터가있습니다 데이터를 지우고 계속 하시겠습니까?";
        }
        else
        {
            GameManager.LoadScene("Lobby_Tutorial");
            // play_inGame(); //없을 경우 씬 변경
        }
    }
    public void SetMusicVolume(float volume) => SoundManager.SetMusicVolume(volume);
    public void SetSFXVolume(float volume) => SoundManager.SetSFXVolume(volume);

    public void Click_loadGame()
    {
        SoundManager.PlaySFX(buttonSound);
        play_inGame();// 인게임 씬으로 변경
    }

    public void Click_option()
    {
        SoundManager.PlaySFX(buttonSound);
        option_Popup.SetActive(true);
    }

    public void Click_QuitGame()
    {
        SoundManager.PlaySFX(buttonSound);
        popUP.SetActive(true);
        popup_Text.text = "게임을 종료하시겠습니까?";
    }

    public void Close_OPtion_Btn()
    {
        SoundManager.PlaySFX(buttonSound);
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
        SoundManager.PlaySFX(buttonSound);
        if(popup_Text.text == "게임을 종료하시겠습니까?")
        {
            Application.Quit();
        }
        else if(popup_Text.text == "저장된 데이터가있습니다 데이터를 지우고 계속 하시겠습니까?")
        {
            GameManager.LoadScene("Lobby_Tutorial");
            // play_inGame();
        }
    }

    public void quit_Btn()
    {
        SoundManager.PlaySFX(buttonSound);
        Application.Quit();
    }

    public void FullScreen_Btn()
    {
        SoundManager.PlaySFX(buttonSound);
        fullScreen_Box.sprite = checkBox;
        windowScreen_Box.sprite = emptyBox;

        Screen.SetResolution(1920, 1080, true);

    }
    public void WindowScreen_btn()
    {
        SoundManager.PlaySFX(buttonSound);
        fullScreen_Box.sprite = emptyBox;
        windowScreen_Box.sprite = checkBox;

        Screen.SetResolution(1600, 900, false);
    }

    public void play_inGame()
    {
        Web_Waitting.SetActive(true);
        DataManager.Instance.LoadGameData((x) => GameManager.LoadScene("Lobby"));
    }

    public void pressBtn()
    {
        SoundManager.PlaySFX(buttonSound);
        pressBtnBG.SetActive(false);
        login_BG.SetActive(true);
    }

    //로그인
    public void SignUp()
    {
        SoundManager.PlaySFX(buttonSound);
        sign_up_BG.SetActive(true);
        login_BG.SetActive(false);
    }
    public void SignUpCancel()
    {
        SoundManager.PlaySFX(buttonSound);
        sign_up_BG.SetActive(false);
        login_BG.SetActive(true);
    }
    public void Login()
    {
        SoundManager.PlaySFX(buttonSound);
        Web_Waitting.SetActive(true);
        DataManager.Instance.LoginUser(login_ID.text, login_PW.text, (x) => {
        // if (login_ID.text == playerdata.player_ID && login_PW.text == playerdata.player_PW)
            if(x)
            {
                //로그인 성공
                login_BG.SetActive(false);
                gameStartBtn_BG.SetActive(true);
            }
            else
            {
                login_popup.SetActive(true);
                login_popup_Text.text = "로그인 실패";
                // play_inGame();
            }
            Web_Waitting.SetActive(false);
        });
        // else if (login_ID.text != playerdata.player_ID)
        // {
        //     login_popup.SetActive(true);
        //     login_popup_Text.text = "아이디를 확인해주세요";
        // }
        // else if(login_PW.text != playerdata.player_PW)
        // {
        //     login_popup.SetActive(true);
        //     login_popup_Text.text = "비밀번호를 확인해주세요";
        // }
    }

    public void Close_LoginPopUp()
    {
        SoundManager.PlaySFX(buttonSound);
        Debug.Log("눌림");
        login_popup.SetActive(false);
    }


    public void CreateAccount()
    {
        SoundManager.PlaySFX(buttonSound);
        playerdata.player_ID = createID.text;
        if (createPW.text == checkPW.text)
        {
            Web_Waitting.SetActive(true);
            playerdata.player_PW = createPW.text;
            DataManager.Instance.CreateUser(createID.text, createPW.text, (x) => {
                if(x)
                {
                    sign_up_BG.SetActive(false);
                    login_BG.SetActive(true);
                }
                else
                {
                    login_popup.SetActive(true);
                    login_popup_Text.text = "생성 실패";
                }
                Web_Waitting.SetActive(false);
            });
        }
        else
        {
            login_popup.SetActive(true);
            login_popup_Text.text = "비밀번호가 같지않습니다.";
        }
        playerdata.player_name = createname.text;

    }
}
