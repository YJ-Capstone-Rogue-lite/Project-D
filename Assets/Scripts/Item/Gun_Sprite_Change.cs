using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gun_Sprite_Change : MonoBehaviour
{
    public Fire_Test fire_Test; //본인 총기 받아올 값

    public SpriteRenderer PistolgunSpriteRenderer1; // 슬롯1 권총의 스프라이트 렌더러를 참조할 변수
    public SpriteRenderer ArandSgSpriteRenderer1; //슬롯1 돌격소총과 샷건의 스프라이트 렌더러를 참조할 변수
    public SpriteRenderer SRSpriteRenderer1; // 슬롯1 돌격소총과 샷건의 스프라이트 렌더러를 참조할 변수



    private void Update()
    {
        // 총의 스프라이트 렌더러가 할당되어 있고, 총의 데이터가 존재할 때
        if (PistolgunSpriteRenderer1 != null && ArandSgSpriteRenderer1 != null && SRSpriteRenderer1 != null && fire_Test.default_weapon != null)
        {
            if (fire_Test.weapon.weaponType == Weapon.WeaponType.Pistol) // 무기타입이 권총일경우
            {
                ArandSgSpriteRenderer1.sprite = null; //먼저 다른 총들 스프라이트 없애버리고
                SRSpriteRenderer1.sprite = null;
                // 권총의 스프라이트를 현재 활성화된 슬롯 데이터의 스프라이트로 설정합니다.
                PistolgunSpriteRenderer1.sprite = fire_Test.weapon.sprite;

            }

            else if (fire_Test.weapon.weaponType == Weapon.WeaponType.Assaultt_Rifle || fire_Test.weapon.weaponType == Weapon.WeaponType.Shoot_Gun)
            {
                SRSpriteRenderer1.sprite = null;
                PistolgunSpriteRenderer1.sprite = null;
                // 총의 스프라이트를 현재 활성화된 슬롯 데이터의 스프라이트로 설정합니다.
                ArandSgSpriteRenderer1.sprite = fire_Test.weapon.sprite;

            }
            else
            {
                PistolgunSpriteRenderer1.sprite = null;
                ArandSgSpriteRenderer1.sprite = null;
                SRSpriteRenderer1.sprite = fire_Test.weapon.sprite;

            }
        }
    }


 
  
}