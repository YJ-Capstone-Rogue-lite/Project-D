using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.UI;
<<<<<<< HEAD
using static UnityEditor.Progress;

public class Item_interaction : MonoBehaviour
{

    private bool pickupActivated = false;  // 아이템 습득 가능할시 True 

    private Item_PickUp item_PickUp;

    [SerializeField]
    private TMP_Text actionText ;  // 행동을 보여 줄 텍스트
=======
using UnityEngine.UIElements;
using static UnityEditor.Progress;

public class Item_interaction : MonoBehaviour
{ //아이템 상호작용코드

    private bool pickupActivated = false;  // 아이템 습득 가능할시 True 
    private Item_PickUp item_PickUp;
    [SerializeField] private TMP_Text actionText ;  // 행동을 보여 줄 텍스트
    [SerializeField] private Weapon_Slot Weapon_Slot; //웨폰 슬롯의 무기슬롯을 받아오기 위함
    public GameObject player_postion;
>>>>>>> Enemy

    private void Start()
    {
        // actionText를 찾아서 초기화합니다. 예를 들어, 해당 텍스트는 같은 게임 오브젝트 내에 있을 수 있습니다.
        actionText = GameObject.Find("actionText").GetComponent<TMP_Text>();
        // actionText를 비활성화합니다.
        actionText.gameObject.SetActive(false);
<<<<<<< HEAD
=======

>>>>>>> Enemy
    }

    private void Update()
    {
        CanPickUp();
<<<<<<< HEAD
=======
        
        //if (Input.GetKeyDown(KeyCode.T))
        //{
        //    PickUp_Item_Change();
        //}
>>>>>>> Enemy
    }


    private void OnTriggerEnter2D(Collider2D collider2D) //만약 트리거 범위에 닿은 거의 태그가 아이템이 포함되어 있으면
    {
        if (collider2D.gameObject.CompareTag("Item"))
<<<<<<< HEAD
        {
=======
        {           
>>>>>>> Enemy
            pickupActivated = true;
            item_PickUp = collider2D.gameObject.GetComponent<Item_PickUp>();
            // 충돌한 객체가 "Item" 태그를 가지고 있으면 행동 텍스트를 활성화합니다.
            actionText.gameObject.SetActive(true);
            actionText.text = item_PickUp.weapon.name + "<b>" + " 획득 " + "<color=yellow>" + "[E]" + "</b>" + "</color>";

        }

<<<<<<< HEAD

=======
>>>>>>> Enemy
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
        if (Input.GetKeyDown(KeyCode.E) && pickupActivated)
        {
<<<<<<< HEAD
            Weapon_Slot weaponSlotScript = FindObjectOfType<Weapon_Slot>(); //웨폰슬롯스크립트는 웨폰슬롯 코드의 값을 가져옴
            if (weaponSlotScript != null) //웨폰슬롯의 값이 비어있지 않을때
            {
                // 무기 슬롯 스크립트가 있다면 해당 무기를 전달
                weaponSlotScript.ReceiveWeapon(item_PickUp.weapon);
            }
            else
            { 
            }
            Debug.Log(item_PickUp.weapon.name + " 획득 했습니다.");  // 인벤토리 넣기
            Destroy(item_PickUp.gameObject);
        }
    }
=======

            Weapon_Slot weaponSlotScript = FindObjectOfType<Weapon_Slot>(); //웨폰슬롯스크립트는 웨폰슬롯 코드의 값을 가져옴
            if (weaponSlotScript != null) //웨폰슬롯의 값이 비어있지 않을때
            {
                PickUp_Item_Change();

                // 무기 슬롯 스크립트가 있다면 해당 무기를 전달
                weaponSlotScript.ReceiveWeapon(item_PickUp.weapon);
                Debug.Log(item_PickUp.weapon.name + " 획득 했습니다.");  // 인벤토리 넣기
                Debug.Log("아이템 생성함");
            }
            Destroy(item_PickUp.gameObject);

        }
    }


    public void PickUp_Item_Change() //테스트용 무기 생성 코드
    {
        Weapon_Slot weaponSlotScript = FindObjectOfType<Weapon_Slot>(); //웨폰슬롯스크립트는 웨폰슬롯 코드의 값을 가져옴

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


>>>>>>> Enemy
}
