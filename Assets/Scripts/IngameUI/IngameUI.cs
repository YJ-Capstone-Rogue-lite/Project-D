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


    private void Update()
    {
        Weapon_Slot = PlayerChar.single.GetComponent<Weapon_Slot>();
        main_slot_sprite.sprite = Weapon_Slot.weaponSlot1.GetComponent<Fire_Test>().weapon.sprite;
        sub_slot_sprite.sprite = Weapon_Slot.weaponSlot2.GetComponent<Fire_Test>().weapon.sprite;

        //플레이어 따라다니는 재장전 애니메이션 이미지
        reload_img.transform.position = Weapon_Slot.transform.position + new Vector3(0, 0, 0);


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

}
