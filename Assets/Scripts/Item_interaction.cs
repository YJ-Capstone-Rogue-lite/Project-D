using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class Item_interaction : MonoBehaviour
{

    private bool pickupActivated = false;  // 아이템 습득 가능할시 True 

    private Item_PickUp item_PickUp;

    [SerializeField]
    private TMP_Text actionText ;  // 행동을 보여 줄 텍스트

    private void Start()
    {
        // actionText를 찾아서 초기화합니다. 예를 들어, 해당 텍스트는 같은 게임 오브젝트 내에 있을 수 있습니다.
        actionText = GameObject.Find("actionText").GetComponent<TMP_Text>();
        // actionText를 비활성화합니다.
        actionText.gameObject.SetActive(false);
    }

    private void Update()
    {
        CanPickUp();
    }


    private void OnTriggerEnter2D(Collider2D collider2D) //만약 트리거 범위에 닿은 거의 태그가 아이템이 포함되어 있으면
    {
        if (collider2D.gameObject.CompareTag("Item"))
        {
            pickupActivated = true;
            item_PickUp = collider2D.gameObject.GetComponent<Item_PickUp>();
            // 충돌한 객체가 "Item" 태그를 가지고 있으면 행동 텍스트를 활성화합니다.
            actionText.gameObject.SetActive(true);
            actionText.text = item_PickUp.weapon.name + "<b>" + " 획득 " + "<color=yellow>" + "[E]" + "</b>" + "</color>";

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
        if (Input.GetKeyDown(KeyCode.E) && pickupActivated)
        {
            Weapon_Slot weaponSlotScript = FindObjectOfType<Weapon_Slot>(); //웨폰슬롯스크립트는 웨폰슬롯 코드의 값을 가져옴
            if (weaponSlotScript != null) //웨폰슬롯의 값이 비어있지 않을때
            {
                // 무기 슬롯 스크립트가 있다면 해당 무기를 전달
                weaponSlotScript.ReceiveWeapon(item_PickUp.weapon);

                Debug.Log(item_PickUp.weapon.name + " 획득 했습니다.");  // 인벤토리 넣기

            }
            Destroy(item_PickUp.gameObject);

        }
    }
}
