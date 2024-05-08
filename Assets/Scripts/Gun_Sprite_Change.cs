using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gun_Sprite_Change : MonoBehaviour
{
    public SpriteRenderer gunSpriteRenderer; // 총의 스프라이트 렌더러를 참조할 변수
    public Fire_Test fire_Test;



    private void Update()
    {
        // 총의 스프라이트 렌더러가 할당되어 있고, 총의 데이터가 존재할 때
        if (gunSpriteRenderer != null && fire_Test.default_weapon != null)
        {
            // 총의 스프라이트를 현재 활성화된 슬롯 데이터의 스프라이트로 설정합니다.
            gunSpriteRenderer.sprite = fire_Test.weapon.sprite;

           
        }
    }

}