using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Sprite_update : MonoBehaviour
{

    public ConsumableItem ConsumableItem; //스크립트테이블
    //public Fire_Test player_Equip_Weapon; //캐릭터가 가지고있는 웨폰값
    public SpriteRenderer ItemSpriteRenderer; // 아이템의 스프라이트 렌더러를 참조할 변수


    private void Update()
    {
        // 아이템의 스프라이트 렌더러가 할당되어 있고, 아이템의 데이터가 존재할 때
        if (ItemSpriteRenderer != null && ConsumableItem.sprite != null)
        {
            // 아이템의 스프라이트를 아이템 데이터의 스프라이트로 설정합니다.
            ItemSpriteRenderer.sprite = ConsumableItem.sprite;
        }

    }
}
