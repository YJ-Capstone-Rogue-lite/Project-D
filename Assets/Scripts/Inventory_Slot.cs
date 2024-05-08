using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;


public class Inventory_Slot : MonoBehaviour
{
    public static Inventory_Slot inv_slot_Instance;
    [SerializeField]  private Weapon_Slot weapon_Slot;

    public Image inv_weapon1_image; // 인벤토리 슬롯의 무기 이미지
    public Image inv_weapon2_image; // 인벤토리 슬롯의 무기 이미지

    public Image inv_accessory_image; // 인벤토리 슬롯의 악세사리 이미지
    public Image inv_consumableItem_image; //인벤슬롯의 소비아이템 이미지

    //public Item item; // 인벤토리 슬롯에 있는 아이템(아직 스크립블 오브젝트가 없음
    public Weapon weapon; // 무기
    public Accessory accessory; //악세사리
    public ConsumableItem consumableItem; //소비아이템

    private void Awake()
    {
        weapon_Slot = PlayerChar.single.GetComponent<Weapon_Slot>();

    }

    

    //무기 추가하고 삭제하는 코드
    public void AddWeapon() //무기를 무기 슬롯에 추가하는 함수
    {
        // 가져온 이미지가 유효한지 확인
        if (weapon_Slot != null)
        {
            // 새 무기의 아이콘을 웨폰 슬롯의 슬롯1 이미지로 설정
            inv_weapon1_image.sprite = weapon_Slot.weaponSlot1.GetComponent<Fire_Test>().weapon.sprite; // newWeaponIcon은 새 무기의 아이콘(sprite)이라고 가정
            inv_weapon2_image.sprite = weapon_Slot.weaponSlot2.GetComponent<Fire_Test>().weapon.sprite; // newWeaponIcon은 새 무기의 아이콘(sprite)이라고 가정
            inv_weapon1_image.enabled = true; // 무기 이미지 활성화
            inv_weapon2_image.enabled = true; // 무기 이미지 활성화

        }
        else
        {
            Debug.LogError("Weapon slot image not found!");
        }
    }
   

}
