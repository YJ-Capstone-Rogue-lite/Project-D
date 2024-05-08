using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public Inventory_Slot[] weaponSlots = new Inventory_Slot[2]; // 무기 슬롯 배열
    public Inventory_Slot[] accessorySlots = new Inventory_Slot[5]; // 악세사리 슬롯 배열
    public Inventory_Slot[] consumableSlot = new Inventory_Slot[1]; // 소비 아이템 슬롯

    // 인벤토리에 웨폰슬롯에 무기 추가하는 함수
    public void AddWeapon_Inventory(Weapon newWeapon)
    {
        // 아이템의 타입에 따라 적절한 슬롯에 추가
        if (newWeapon.weaponType != Weapon.WeaponType.None) //newitem의 무기 타입이 none이 아닌경우(즉 무기인 경우)
        {
            // 무기 아이템인 경우, 무기 슬롯 배열을 순회하면서 빈 슬롯을 찾아 아이템을 추가(그러나 무기 슬롯과 연동해서 넣을 가능성높음
            for (int i = 0; i < weaponSlots.Length; i++)
            {
                if (weaponSlots[i].weapon == null) //웨폰슬롯의 배열에 무기가 없으면 
                {
                    weaponSlots[i].AddWeapon(newWeapon); //비어있는 웨폰슬롯에 무기 추가
                    return; // 아이템을 추가한 후 함수를 종료합니다.
                }
            }
        }
    }


    // 인벤토리에 아이템 슬롯에 악세사리 및 소비아이템 추가하는 함수

    public void AddIAccessory_Inventory(Accessory newAccessory)
    {
         if (newAccessory.itemType != Accessory.ItemType.None) //악세사리의 아이템 타입이 None 아닌경우
         {
            // 악세사리 아이템인 경우, 악세사리 슬롯 배열을 순회하면서 빈 슬롯을 찾아 아이템을 추가합니다.
            for (int i = 0; i < accessorySlots.Length; i++)
            {
                if (accessorySlots[i].accessory == null)
                {
                    accessorySlots[i].AddAccessory(newAccessory);
                    return; // 아이템을 추가한 후 함수를 종료합니다.
                }
            }
         }
 
    }


    // 인벤토리에 소비슬롯에 소비아이템 추가하는 함수
    public void AddConsumableItem_Inventory(ConsumableItem newConsumableItem)
    {
        // 아이템의 타입에 따라 적절한 슬롯에 추가
        if (newConsumableItem.itemType != ConsumableItem.ItemType.None) //newitem의 무기 타입이 none이 아닌경우(즉 무기인 경우)
        {      
                if (consumableSlot[0].consumableItem == null) //소비아이템 슬롯의 배열에 아무것도 없으면 
                {
                consumableSlot[0].AddConsumableItem(newConsumableItem); //비어있는 소비슬롯에 소비템 추가
                    return; // 아이템을 추가한 후 함수를 종료합니다.
                }
            
        }
    }

}
