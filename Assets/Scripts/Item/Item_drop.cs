using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_drop : MonoBehaviour
{
    public GameObject[] ConsumableItem_drop_prefeb; //드랍될 소비아이템을 저장해둔 배열
    public float dropChance = 0.5f; // 아이템 드랍 확률 (0.5는 50%)
    

    //몬스터가 죽으면 일정 확률로 포션 또는 소비아이템 드랍
    public void enemy_item_drop()
    {
        // 확률에 따라 아이템 드랍 결정
        if (Random.value <= dropChance)
        {
            // 배열에서 랜덤한 아이템 선택
            int randomIndex = Random.Range(0, ConsumableItem_drop_prefeb.Length);

            // 선택된 아이템 프리팹을 현재 위치에 생성
            Instantiate(ConsumableItem_drop_prefeb[randomIndex], gameObject.transform.position, Quaternion.identity);
        }
    }
}
