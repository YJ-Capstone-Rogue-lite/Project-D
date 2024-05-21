using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class IngameUI : MonoBehaviour
{
    [SerializeField] private Weapon_Slot Weapon_Slot;

    public Image main_slot_sprite;
    public Image sub_slot_sprite;

    public Animator MainWeapon_Swap;
    public Animator SubWeapon_Swap;

    public bool MainWeapon = false; // true면 MainWeapon false면 SubWeapon
    public bool SubWeapon = true; // true면 MainWeapon false면 SubWeapon

    [SerializeField]  private GameObject reload_img;

    public GameObject inv_slot;
    [SerializeField] private bool inv_slot_active_bool;
    public Inventory_Slot inventory_slot;

    private void Start()
    {
        inv_slot_active_bool = false;
    }

    private void Update()
    {
        Weapon_Slot = PlayerChar.single.GetComponent<Weapon_Slot>();
        main_slot_sprite.sprite = Weapon_Slot.weaponSlot1.GetComponent<Fire_Test>().weapon.sprite;
        sub_slot_sprite.sprite = Weapon_Slot.weaponSlot2.GetComponent<Fire_Test>().weapon.sprite;

        //플레이어 따라다니는 재장전 애니메이션 이미지
        reload_img.transform.position = Weapon_Slot.transform.position + new Vector3(0, 0, 0);

        if (Input.GetKeyDown(KeyCode.Tab)) //탭키를 눌렀을때
        {
            inventory_open(); //인벤토리 오픈
        }

        if (Input.GetButtonDown("MainWeapon") && MainWeapon == true)
        {
            MainWeapon_Swap.SetBool("MainWeapon_Up", true);
            SubWeapon_Swap.SetBool("SubWeapon_Down", true);
            MainWeapon_Swap.SetBool("MainWeapon_Down", false);
            SubWeapon_Swap.SetBool("SubWeapon_Up", false);
            MainWeapon = false;
            SubWeapon = true;
            Debug.Log("MainClick");
            

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

        }
    }

    public void inventory_open() //탭키를 눌렀을때
    {
        if (inv_slot_active_bool == false) //만약 인벤슬롯의 불 값이 false면
        {
            inv_refresh(); //인벤토리 갱신
        }
        else
        {
            inv_slot.SetActive(false); //false가 아니라면 인벤토리 슬롯 끄기
            inv_slot_active_bool = false; //인벤슬롯 끄고 다시 false로 바꿔주기
        }
    }
    public void inv_refresh() //인벤토리 갱신
    {
        inv_slot.SetActive(true); //인벤슬롯 활성화
        inv_slot_active_bool = true; //불값 true
        inventory_slot.AddWeapon(); //인벤 슬롯에 무기 할당 코드
        Debug.Log("인벤토리 열림");

    }
}

