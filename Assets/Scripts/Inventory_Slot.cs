using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.TextCore.Text;


public class Inventory_Slot : MonoBehaviour, IPointerEnterHandler
{
    public static Inventory_Slot inv_slot_Instance;
    [SerializeField]  private Weapon_Slot weapon_Slot;
    private IngameUI ingameUI;
    private Item_interaction item_Interaction;
    public Character character;

    public Image inv_weapon1_image; // 인벤토리 슬롯의 무기 이미지
    public Image inv_weapon2_image; // 인벤토리 슬롯의 무기 이미지
    public Image item_info_image; //아이템 설명창 이미지


    public Image inv_accessory_image; // 인벤토리 슬롯의 악세사리 이미지
    public Image inv_consumableItem_image; //인벤 슬롯의 소비아이템 이미지

    public TMP_Text item_info_text; //아이템 설명 텍스트

    [Header("패시브 버프 관련")]
    public Image Stack_Buff_Attack; //스택형 공버프 전체
    public Image Stack_Buff_Attack_Icon; //스택형 공버프 아이콘
    public TMP_Text Stack_Buff_Attack_Count; //스택형 공버프 카운트
    public Image Stack_Buff_Attack_text_background;//설명 문구 뜨는 배경
    public TMP_Text Stack_Buff_Attack_text;// 설명 문구 뜨는 텍스트


    public Image Stack_Buff_Speed; //스택형 (설명 문구 뜨게 하는 용)
    public Image Stack_Buff_Speed_Icon; //스택형 이속버프 아이콘
    public TMP_Text Stack_Buff_Speed_Count; //스택형 이속버프 카운트
    public Image Stack_Buff_Speed_text_background;//설명 문구 뜨는 배경
    public TMP_Text Stack_Buff_Speed_text;// 설명 문구 뜨는 텍스트

    public Image Stack_Buff_Health; //스택형 공버프 전체(설명 문구 뜨게 하는 용)
    public Image Stack_Buff_Health_Icon; //스택형 체력버프 아이콘
    public TMP_Text Stack_Buff_Health_Count; //스택형 체력버프 카운트
    public Image Stack_Buff_Health_text_background;//설명 문구 뜨는 배경
    public TMP_Text Stack_Buff_Health_text;// 설명 문구 뜨는 텍스트
    


    //public Item item; // 인벤토리 슬롯에 있는 아이템(아직 스크립블 오브젝트가 없음
    public Weapon weapon; // 무기
    public Accessory accessory; //악세사리
    public ConsumableItem consumableItem; //소비아이템


    private void Awake()
    {
        weapon_Slot = PlayerChar.single.GetComponent<Weapon_Slot>();
        ingameUI = FindObjectOfType<IngameUI>();
        item_Interaction = FindObjectOfType<Item_interaction>();
        character = FindAnyObjectByType<Character>();

        // 1. 해당 이미지 객체에 EventTrigger 컴포넌트 추가
        EventTrigger trigger1 = inv_weapon1_image.gameObject.AddComponent<EventTrigger>();
        EventTrigger trigger2 = inv_weapon2_image.gameObject.AddComponent<EventTrigger>();
        EventTrigger trigger3 = inv_consumableItem_image.gameObject.AddComponent<EventTrigger>();


        // 2. PointerEnter 이벤트에 대한 콜백 함수 설정
        EventTrigger.Entry entry1 = new EventTrigger.Entry();
        entry1.eventID = EventTriggerType.PointerEnter;
        entry1.callback.AddListener((data) => { OnPointerEnter((PointerEventData)data); });
        trigger1.triggers.Add(entry1);

        EventTrigger.Entry entry2 = new EventTrigger.Entry();
        entry2.eventID = EventTriggerType.PointerEnter;
        entry2.callback.AddListener((data) => { OnPointerEnter((PointerEventData)data); });
        trigger2.triggers.Add(entry2);

        EventTrigger.Entry entry3 = new EventTrigger.Entry();
        entry3.eventID = EventTriggerType.PointerEnter;
        entry3.callback.AddListener((data) => { OnPointerEnter((PointerEventData)data); });
        trigger3.triggers.Add(entry3);

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

    //무기 추가하고 삭제하는 코드
    public void AddConsumableItem() //소비템을 슬롯에 추가하는 함수
    {
        // 가져온 이미지가 유효한지 확인
        if (ingameUI.ConsumableItem_Img.sprite != null)
        {
            // 인벤토리의 소비슬롯 이미지를 인게임 ui의 소비 슬롯 스프라이트의 값으로 교체
            inv_consumableItem_image.sprite = ingameUI.ConsumableItem_Img.sprite;
            inv_consumableItem_image.enabled = true; // 이미지 활성화

        }
        else
        {
            Debug.LogError("소비슬롯 이미지를 찾을 수 없음");
        }
    }

    public void AddBuff_Effect() //인벤토리에 버프 아이콘 표시
    {
        if (character == null)
        {
            Debug.LogError("character 객체가 null입니다. 초기화가 필요합니다.");
            return; // 함수 종료
        }
       
            Stack_Buff_Attack_Count.text = character.damageUpStack.ToString();
            Stack_Buff_Health_Count.text = character.max_hp_UPStack.ToString();
            Stack_Buff_Speed_Count.text = character.movement_SpeedUpStack.ToString();
            Debug.Log("버프 효과 적용됨" + Stack_Buff_Attack_Count);
       
    }

    public void OnPointerEnter(PointerEventData eventData) // 마우스 들어올때 인벤토리 옆에 설명 문구 뜨게 하기
    {
        if (inv_weapon1_image.gameObject == eventData.pointerEnter) //1번슬롯 설명창에 띄우기
        {
            item_info_text.text = weapon_Slot.weaponSlot1.GetComponent<Fire_Test>().weapon.info;
            Debug.Log("아이템1 설명 텍스트 실행");

            item_info_image.sprite = weapon_Slot.weaponSlot1.GetComponent<Fire_Test>().weapon.sprite;
            Debug.Log("아이템1 설명 이미지 실행");
        }
        else if (weapon_Slot.weaponSlot2.GetComponent<Fire_Test>().weapon.weaponType != Weapon.WeaponType.None && inv_weapon2_image.gameObject == eventData.pointerEnter) //2번슬롯 설명창에 띄우기, 2번 무기칸 타입ㅇ none이 아닐때 실행
        {
            item_info_text.text = weapon_Slot.weaponSlot2.GetComponent<Fire_Test>().weapon.info;
            Debug.Log("아이템2 설명 텍스트 실행");

            item_info_image.sprite = weapon_Slot.weaponSlot2.GetComponent<Fire_Test>().weapon.sprite;
            Debug.Log("아이템2 설명 이미지 실행");
        }
        //인게임 ui의 소비 슬롯의 스프라이트가 존재하고 인게임 ui의 스프라이트가 기본값이 아닐때
        else if (inv_consumableItem_image.sprite != ingameUI.default_consumableItem.sprite && inv_consumableItem_image.gameObject == eventData.pointerEnter)
        {

            item_info_image.sprite = inv_consumableItem_image.sprite;
            Debug.Log("소비슬롯 설명 이미지 실행");

            item_info_text.text = item_Interaction.currentConsumable.info;
            Debug.Log("소비슬롯 설명 텍스트 실행");

        }
    }

}
