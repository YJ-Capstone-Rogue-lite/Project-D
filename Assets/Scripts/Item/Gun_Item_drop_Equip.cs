using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Gun_Item_drop_Equip : MonoBehaviour
{
    public Weapon Weapon; //웨폰 스크립트테이블
    //public Fire_Test player_Equip_Weapon; //캐릭터가 가지고있는 웨폰값
    public SpriteRenderer gunSpriteRenderer; // 총의 스프라이트 렌더러를 참조할 변수


    private void Update()
    {
        // 총의 스프라이트 렌더러가 할당되어 있고, 총의 데이터가 존재할 때
        if (gunSpriteRenderer != null && Weapon.sprite != null)
        {
            // 총의 스프라이트를 총 데이터의 스프라이트로 설정합니다.
            gunSpriteRenderer.sprite = Weapon.sprite;
        }

    }

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.gameObject.CompareTag("Player"))
    //    {
    //        //Destroy(this.gameObject); //item_interaction(아이템 상호작용코드)고려해서 주석처리
    //        //Debug.Log(gameObject.name + " 아이템 먹음");

    //        Weapon_Slot weaponSlotScript = FindObjectOfType<Weapon_Slot>(); //웨폰슬롯스크립트는 웨폰슬롯 코드의 값을 가져옴
    //        if (weaponSlotScript != null) //웨폰슬롯의 값이 비어있지 않을때
    //        {
    //            // 무기 슬롯 스크립트가 있다면 해당 무기를 전달
    //            weaponSlotScript.ReceiveWeapon(Weapon);
    //        }
    //        else
    //        {
    //            Debug.LogError("Weapon_Slot 스크립트를 찾을 수 없습니다.");
    //        }
    //    }
    //}


}