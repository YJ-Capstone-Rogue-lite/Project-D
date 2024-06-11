using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;
using UnityEngine.UIElements;
using static UnityEditor.Progress;

public class Item_interaction : MonoBehaviour
{ //아이템 상호작용코드

    private bool pickupActivated = false;  // 아이템 습득 가능할시 True 
    public Item_PickUp item_PickUp;
    [SerializeField] private TMP_Text actionText;  // 행동을 보여 줄 텍스트
    //[SerializeField] private Weapon_Slot Weapon_Slot;
    public GameObject player_postion;
    private Weapon_Slot weaponSlotScript; //웨폰 슬롯의 무기슬롯을 받아오기 위함
    [SerializeField]  private IngameUI ingameUI;
    [SerializeField] private Character character;


    private void Start()
    {
        // actionText를 찾아서 초기화합니다. 예를 들어, 해당 텍스트는 같은 게임 오브젝트 내에 있을 수 있습니다.
        actionText = GameObject.Find("actionText").GetComponent<TMP_Text>();
        // actionText를 비활성화합니다.
        actionText.gameObject.SetActive(false);


    }

    private void Awake()
    {
        weaponSlotScript = FindObjectOfType<Weapon_Slot>(); //웨폰슬롯스크립트는 웨폰슬롯 코드의 값을 가져옴
        ingameUI = FindObjectOfType<IngameUI>();
        character = FindAnyObjectByType<Character>();

    }

    private void Update()
    {
        CanPickUp();
        active_Potion();
        active_Shield();
        //if (Input.GetKeyDown(KeyCode.T))
        //{
        //    PickUp_Item_Change();
        //}
    }


    private void OnTriggerEnter2D(Collider2D collider2D) //만약 트리거 범위에 닿은 거의 태그가 아이템이 포함되어 있으면
    {
        if (collider2D.gameObject.CompareTag("Item"))
        {
            pickupActivated = true;
            item_PickUp = collider2D.gameObject.GetComponent<Item_PickUp>();
            // 충돌한 객체가 "Item" 태그를 가지고 있으면 행동 텍스트를 활성화합니다.
            actionText.gameObject.SetActive(true);
            if (item_PickUp.weapon != null) //만약 아이템 픽업의 코드에 웨폰이 존재한다면
            {
                actionText.text = item_PickUp.weapon.name + "<b>" + " 획득 " + "<color=yellow>" + "[E]" + "</b>" + "</color>";

            }
            else if (item_PickUp.consumable != null)
            {
                actionText.text = item_PickUp.consumable.name + "<b>" + " 획득 " + "<color=yellow>" + "[E]" + "</b>" + "</color>";

            }

        }

    }

    private void OnTriggerExit2D(Collider2D collider2D)
    {
        if (collider2D.gameObject.CompareTag("Item"))
        {
            pickupActivated = false;
            actionText.gameObject.SetActive(false);
        }


    }

    //해당 코드는 임시로 무기만 넣도록 만들어둠. 악세사리 및 버프기도 해야함
    //현재 UI의 1,2번 슬롯에만 들어가고 있어서 인벤토리의 무기 슬롯에 들어가는 작업도 따로 해줘야함.
    private void CanPickUp()
    {
        if (Input.GetKeyDown(KeyCode.E) && pickupActivated) //E를 누르고 픽업이 할성화 될때
        {

            if (weaponSlotScript != null && item_PickUp.weapon != null) //웨폰슬롯의 값이 비어있지 않을때
            {
                PickUp_Weapon_Change();

                // 무기 슬롯 스크립트가 있다면 해당 무기를 전달
                weaponSlotScript.ReceiveWeapon(item_PickUp.weapon);
                Debug.Log(item_PickUp.weapon.name + " 획득 했습니다.");  // 인벤토리 넣기
                Debug.Log("아이템 생성함");
            }
            else if (item_PickUp.consumable != null)
            {
                PickUp_Item_Change();

                // 소비 아이템을 인게임 UI에 표시
                ingameUI.ConsumableItem_Img.sprite = item_PickUp.consumable.sprite;
                Debug.Log(item_PickUp.consumable.name + " 획득 했습니다.");  // 인벤토리 넣기
                Debug.Log("아이템 생성함");
            }
            Destroy(item_PickUp.gameObject);

        }
    }


    public void PickUp_Weapon_Change() //아이템 픽업시 프리펩 재생성 코드
    {
        //무기 코드
        if (weaponSlotScript != null && weaponSlotScript.activeWeaponSlot != null) //웨폰 슬롯 코드와 웨폰슬롯의 활성화된 무기가 null이 아닐때
        {
            // 활성화된 무기 슬롯의 Fire_Test 컴포넌트를 가져옴
            Fire_Test activeWeaponFireTest = weaponSlotScript.activeWeaponSlot.GetComponent<Fire_Test>();

            if (activeWeaponFireTest != null && activeWeaponFireTest.weapon != null)
            {
                // 웨폰 슬롯 2의 무기 타입이 None이 아닌 경우에만 프리팹을 생성합니다.
                if (weaponSlotScript.weaponSlot2.GetComponent<Fire_Test>().weapon.weaponType != Weapon.WeaponType.None)
                {
                    // 아이템의 프리팹을 가져옵니다.
                    GameObject itemPrefab = activeWeaponFireTest.weapon.weapnPrefab;

                    if (itemPrefab != null)
                    {
                        // 플레이어 위치를 가져와서 아이템을 생성할 위치로 설정합니다.
                        Vector3 spawnPosition = player_postion.transform.position;
                        spawnPosition.z = 0f; // z 축 위치를 0으로 설정 (2D 게임에서는 z 축이 필요 없음)

                        // 아이템 프리팹을 복제하여 새로운 아이템을 생성합니다.
                        GameObject newItem = Instantiate(itemPrefab, spawnPosition, Quaternion.identity);
                    }
                    else
                    {
                        Debug.LogError("현재 무기의 프리팹이 없습니다.");
                    }
                }
                else
                {
                    Debug.Log("무기 슬롯 2의 무기가 None입니다. 새로운 아이템을 생성하지 않습니다.");
                }
            }
            else
            {
                Debug.LogError("활성화된 무기의 Fire_Test 컴포넌트 또는 무기 정보를 찾을 수 없습니다.");
            }
        }
        else
        {
            Debug.Log("현재 무기 슬롯에 아이템이 없습니다.");
        }

    }


    //----------소비슬롯 코드---------//
    public void PickUp_Item_Change()
    {
        //만약 아이템 픽업의 소비아이템이 존재하고 인게임 ui의 소비슬롯 이미지가 존재하고 소비아이템슬롯의 스프라이트가 기본값이 아닌 경우에만
        if (item_PickUp.consumable != null && ingameUI.ConsumableItem_Img.sprite != null && ingameUI.ConsumableItem_Img.sprite != ingameUI.default_consumableItem.sprite) //!= ConsumableItem.ItemType.None)
        {
            Debug.Log("소비아이템과 소비 슬롯 이미지가 존재합니다."); // 디버그 문장 추가

            //소비아이템이 가지고있는 프리펩 할당
            GameObject ConsumItemPrefab = item_PickUp.consumable.ConsumItemPrefab;

            if (ConsumItemPrefab != null)
            {
                Debug.Log("프리팹이 존재합니다."); // 디버그 문장 추가

                // 플레이어 위치를 가져와서 아이템을 생성할 위치로 설정합니다.
                Vector3 spawnPosition = player_postion.transform.position;
                spawnPosition.z = 0f; // z 축 위치를 0으로 설정 (2D 게임에서는 z 축이 필요 없음)

                // 아이템 프리팹을 복제하여 새로운 아이템을 생성합니다.
                GameObject new_Consum_Item = Instantiate(ConsumItemPrefab, spawnPosition, Quaternion.identity);
            }
            else
            {
                Debug.LogWarning("프리펩이 존재하지 않습니다."); // 프리펩이 없는 경우에 대한 경고 추가
            }
        }
        else
        {
            Debug.LogWarning("소비아이템이나 소비 슬롯 이미지가 존재하지 않습니다."); // 소비아이템이나 소비 슬롯 이미지가 없는 경우에 대한 경고 추가
        }

    }
    //포션 사용 기능
    public void active_Potion()
    {

        // 왼쪽 Shift 키를 누르고, 인게임 슬롯의 스프라이트가 기본값이 아니며, 캐릭터의 현재 체력이 최대 체력이 아닌 경우에만
        if (Input.GetKeyDown(KeyCode.LeftShift) &&
            ingameUI.ConsumableItem_Img.sprite != null &&
            ingameUI.ConsumableItem_Img.sprite != ingameUI.default_consumableItem.sprite &&
            character.m_health < character.m_maxHealth)
            {
            Debug.Log("쉬프트 키를 누르고 모든 조건을 충족함");
            // item_PickUp 또는 consumable이 null인 경우를 대비한 검사 필요
            if (item_PickUp.consumable != null &&
                item_PickUp.consumable.itemType == ConsumableItem.ItemType.Potion)
            {
                character.EffectAction(EffectDatas.effectDatas[0]);

                // 캐릭터의 현재 체력 값에 포션의 힐링 값 더하기
                character.m_health += item_PickUp.consumable.HPHealing;
                Debug.Log("캐릭터의 체력을 " + item_PickUp.consumable.HPHealing +" 만큼 회복합니다.");
                Debug.Log("현재 캐릭터의 체력 : " +  character.m_health);

                // 만약 회복시 현재 체력이 최대 체력보다 더 커지면 최대 체력으로 설정
                if (character.m_health > character.m_maxHealth)
                {
                    character.m_health = character.m_maxHealth;
                    Debug.Log("캐릭터의 체력이 최대체력을 넘어서 최대체력으로 변경");
                }
                character.player_hpbar_update();

                // 회복을 시키면 소비템 슬롯의 스프라이트를 기본값으로 변경
                ingameUI.ConsumableItem_Img.sprite = ingameUI.default_consumableItem.sprite;
                Debug.Log("소비템을 사용해서 슬롯의 이미지를 기본값으로 변경");
            }
        }
    }

    //쉴드 사용 기능
    public void active_Shield()
    {

        // 왼쪽 Shift 키를 누르고, 인게임 슬롯의 스프라이트가 기본값이 아니며, 캐릭터의 현재 쉴드가 최대 쉴드보다 작은 경우에만
        if (Input.GetKeyDown(KeyCode.LeftShift) &&
            ingameUI.ConsumableItem_Img.sprite != null &&
            ingameUI.ConsumableItem_Img.sprite != ingameUI.default_consumableItem.sprite &&
            character.m_shield < character.m_maxShield)
        {
            Debug.Log("쉬프트 키를 누르고 모든 조건을 충족함");
            // item_PickUp 또는 consumable이 null인 경우를 대비한 검사 필요
            if (item_PickUp.consumable != null &&
                item_PickUp.consumable.itemType == ConsumableItem.ItemType.Shield)
            {
                // 캐릭터의 현재 쉴드 값에 아이템 쉴드 값 더하기
                character.m_shield += item_PickUp.consumable.AddShield;
                Debug.Log("캐릭터의 쉴드 " + item_PickUp.consumable.AddShield + " 만큼 회복합니다.");
                Debug.Log("현재 캐릭터의 쉴드 : " + character.m_shield);

                // 만약 회복시 현재 체력이 최대 체력보다 더 커지면 최대 체력으로 설정
                if (character.m_shield > character.m_maxShield)
                {
                    character.m_shield = character.m_maxShield;
                    Debug.Log("캐릭터의 현재 쉴드가 최대 쉴드량을 넘어서 현재 쉴드를 최대 쉴드량으로 변경");
                }
                character.player_shieldbar_update();

                // 회복을 시키면 소비템 슬롯의 스프라이트를 기본값으로 변경
                ingameUI.ConsumableItem_Img.sprite = ingameUI.default_consumableItem.sprite;
                Debug.Log("소비템을 사용해서 슬롯의 이미지를 기본값으로 변경");
            }
        }
    }
}