using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;


public class Inventory_Slot : MonoBehaviour
{
    public Image inv_weapon_image; // 인벤토리 슬롯의 무기 이미지
    public Image inv_accessory_image; // 인벤토리 슬롯의 악세사리 이미지
    public Image inv_consumableItem_image; //인벤슬롯의 소비아이템 이미지

    //public Item item; // 인벤토리 슬롯에 있는 아이템(아직 스크립블 오브젝트가 없음
    public Weapon weapon; // 무기
    public Accessory accessory; //악세사리
    public ConsumableItem consumableItem; //소비아이템



    //무기 추가하고 삭제하는 코드
    public void AddWeapon(Weapon newWeapon) //무기를 무기 슬롯에 추가하는 함수
    {
        weapon = newWeapon;
        inv_weapon_image.sprite = weapon.sprite; // 슬롯의 아이콘을 새 무기의 아이콘으로 설정
        inv_weapon_image.enabled = true; //무기 이미지 활성화
    }
    
    public void ClearWeaponSlot()
    {
        weapon = null;
        inv_weapon_image.sprite = null; //무기 스프라이트 제거
        inv_weapon_image.enabled = false; // 무기 이미지 비활성화
    }
    
    

     //악세서리를 슬롯에 추가
    public void AddAccessory(Accessory newAccessory)
    {
        accessory = newAccessory;
        inv_accessory_image.sprite = accessory.sprite; // 인벤토리의 악세사리 슬롯의 스프라이트 교체
        inv_accessory_image.enabled = true; // 이미지 활성화


    }

    //슬롯을 비우는 함수
    public void ClearAccessorySlot()
    {
        accessory = null; // 아이템 제거
        inv_accessory_image.sprite = null; // 아이콘 제거
        inv_accessory_image.enabled = false; // 아이콘 비활성화

    }

    //소비아이템을 슬롯에 추가
    public void AddConsumableItem(ConsumableItem newConsumableItem)
    {
        consumableItem = newConsumableItem;
        inv_consumableItem_image.sprite = consumableItem.sprite; // 인벤토리의 악세사리 슬롯의 스프라이트 교체
        inv_consumableItem_image.enabled = true; // 이미지 활성화


    }

    //슬롯을 비우는 함수
    public void ClearConsumableItemSlot()
    {
        consumableItem = null; // 아이템 제거
        inv_consumableItem_image.sprite = null; // 아이콘 제거
        inv_consumableItem_image.enabled = false; // 아이콘 비활성화

    }


}
