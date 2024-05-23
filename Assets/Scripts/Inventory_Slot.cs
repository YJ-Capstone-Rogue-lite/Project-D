using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
<<<<<<< HEAD
using static UnityEditor.Progress;


public class Inventory_Slot : MonoBehaviour
{
    public Image inv_weapon_image; // 인벤토리 슬롯의 무기 이미지
    public Image inv_accessory_image; // 인벤토리 슬롯의 악세사리 이미지
    public Image inv_consumableItem_image; //인벤슬롯의 소비아이템 이미지

=======
using TMPro;
using static UnityEditor.Progress;
using UnityEngine.EventSystems;


public class Inventory_Slot : MonoBehaviour, IPointerEnterHandler
{
    public static Inventory_Slot inv_slot_Instance;
    [SerializeField]  private Weapon_Slot weapon_Slot;

    public Image inv_weapon1_image; // 인벤토리 슬롯의 무기 이미지
    public Image inv_weapon2_image; // 인벤토리 슬롯의 무기 이미지
    public Image itme_info_imgae; //아이템 설명창 이미지


    public Image inv_accessory_image; // 인벤토리 슬롯의 악세사리 이미지
    public Image inv_consumableItem_image; //인벤슬롯의 소비아이템 이미지

    public TMP_Text item_info_text; //아이템 설명 텍스트


>>>>>>> Enemy
    //public Item item; // 인벤토리 슬롯에 있는 아이템(아직 스크립블 오브젝트가 없음
    public Weapon weapon; // 무기
    public Accessory accessory; //악세사리
    public ConsumableItem consumableItem; //소비아이템


<<<<<<< HEAD

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


=======
    private void Awake()
    {
        weapon_Slot = PlayerChar.single.GetComponent<Weapon_Slot>();


        // 1. 해당 이미지 객체에 EventTrigger 컴포넌트 추가
        EventTrigger trigger1 = inv_weapon1_image.gameObject.AddComponent<EventTrigger>();
        EventTrigger trigger2 = inv_weapon2_image.gameObject.AddComponent<EventTrigger>();


        // 2. PointerEnter 이벤트에 대한 콜백 함수 설정
        EventTrigger.Entry entry1 = new EventTrigger.Entry();
        entry1.eventID = EventTriggerType.PointerEnter;
        entry1.callback.AddListener((data) => { OnPointerEnter((PointerEventData)data); });
        trigger1.triggers.Add(entry1);

        EventTrigger.Entry entry2 = new EventTrigger.Entry();
        entry2.eventID = EventTriggerType.PointerEnter;
        entry2.callback.AddListener((data) => { OnPointerEnter((PointerEventData)data); });
        trigger2.triggers.Add(entry2);
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


    public void OnPointerEnter(PointerEventData eventData) // 마우스 들어올때 인벤토리 옆에 설명 문구 뜨게 하기
    {
        if(inv_weapon1_image.gameObject == eventData.pointerEnter) //1번슬롯 설명창에 띄우기
        {
            item_info_text.text = weapon_Slot.weaponSlot1.GetComponent<Fire_Test>().weapon.info;
            Debug.Log("아이템1 설명 텍스트 실행");

            itme_info_imgae.sprite = weapon_Slot.weaponSlot1.GetComponent<Fire_Test>().weapon.sprite;
            Debug.Log("아이템1 설명 이미지 실행");
        }
        else if (weapon_Slot.weaponSlot2.GetComponent<Fire_Test>().weapon.weaponType != Weapon.WeaponType.None &&  inv_weapon2_image.gameObject == eventData.pointerEnter) //2번슬롯 설명창에 띄우기, 2번 무기칸 타입ㅇ none이 아닐때 실행
        {
            item_info_text.text = weapon_Slot.weaponSlot2.GetComponent<Fire_Test>().weapon.info;
            Debug.Log("아이템2 설명 텍스트 실행");

            itme_info_imgae.sprite = weapon_Slot.weaponSlot2.GetComponent<Fire_Test>().weapon.sprite;
            Debug.Log("아이템2 설명 이미지 실행");
        }
    }

>>>>>>> Enemy
}
