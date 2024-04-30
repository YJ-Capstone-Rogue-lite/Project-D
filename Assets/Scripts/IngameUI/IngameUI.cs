using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IngameUI : MonoBehaviour
{
    public Animator MainWeapon_Swap;
    public Animator SubWeapon_Swap;

    public bool MainWeapon = false; // true면 MainWeapon false면 SubWeapon
    public bool SubWeapon = true; // true면 MainWeapon false면 SubWeapon
    public bool openOption = true;

    [SerializeField] private RectTransform WeaponSlot1;
    [SerializeField] private RectTransform WeaponSlot2;


    [SerializeField] private GameObject ingameOption;
    [SerializeField] private GameObject option_popup;
    [SerializeField] private GameObject quit_popup;

    [SerializeField] private Image fullScreen_Box;
    [SerializeField] private Image windowScreen_Box;

    [SerializeField] private Sprite checkBox;
    [SerializeField] private Sprite emptyBox;
    private void Update()
    {
        if (Input.GetButtonDown("MainWeapon") && MainWeapon == true)
        {
            MainWeapon_Swap.SetBool("MainWeapon_Up", true);
            SubWeapon_Swap.SetBool("SubWeapon_Down", true);
            MainWeapon_Swap.SetBool("MainWeapon_Down", false);
            SubWeapon_Swap.SetBool("SubWeapon_Up", false);
            MainWeapon = false;
            SubWeapon = true;
            Debug.Log("MainClick");
            WeaponSlot1.SetAsLastSibling();
            WeaponSlot2.SetAsFirstSibling();
        }
        else if (Input.GetButtonDown("SubWeapon") && SubWeapon == true)
        {
            MainWeapon_Swap.SetBool("MainWeapon_Down", true);
            SubWeapon_Swap.SetBool("SubWeapon_Up", true);
            MainWeapon_Swap.SetBool("MainWeapon_Up", false);
            SubWeapon_Swap.SetBool("SubWeapon_Down", false);
            MainWeapon = true;
            SubWeapon = false;
            Debug.Log("SubClick");
            WeaponSlot2.SetAsLastSibling();
            WeaponSlot1.SetAsFirstSibling();
        }

        if (Input.GetButtonDown("Option") && openOption)
        {
            Debug.Log("open Option");
            ingameOption.SetActive(true);
            openOption = false;
            Time.timeScale = 0f;
            GameManager.Instance.isPlaying = false;
            //인게임 시간 멈추게 하기
        }
        else if (Input.GetButtonDown("Option") && !openOption)
        {
            Debug.Log("open Option");
            ingameOption.SetActive(false);
            openOption = true;
            Time.timeScale = 1f;
            GameManager.Instance.isPlaying = true;
            //인게임 다시 시작
        }

    }

    public void Resume_Btn()
    {
        ingameOption.SetActive(false);
        openOption = true;
    }
    public void Option_Btn()
    {
        option_popup.SetActive(true);
    }

    public void Cancle_Btn()
    {
        option_popup.SetActive(false);
        quit_popup.SetActive(false);

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
    public void Quit_Btn()
    {
        quit_popup.SetActive(true);
    }
    public void QuitGame()
    {
        Application.Quit();
    }

}
