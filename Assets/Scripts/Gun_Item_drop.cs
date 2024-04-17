using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Gun_Item_drop : MonoBehaviour
{
    public Weapon Weapon; //웨폰 스크립트테이블
    public FIre_Test player_Equip_Weapon; //캐릭터가 가지고있는 웨폰값
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            player_Equip_Weapon.weapon = Weapon;
            Debug.Log(Weapon.name + "아이톔으로 교체");
            Destroy(this.gameObject);
            Debug.Log(gameObject.name + " 아이템 먹음");

        }

    }

}