using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_drop : MonoBehaviour
{
    public static Item_drop single;
    public GameObject[] ConsumableItem_drop_prefeb; //드랍될 소비아이템을 저장해둔 배열
    public GameObject[] Weapon_drop_prefeb; //드랍될 무기를 저장해둔 배열

    public float dropChance = 0.5f; // 아이템 드랍 확률 (0.5는 50%)
    public Item_interaction item_interaction;

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

    public void Box_Open_Item_Drop()
    {
        item_interaction = FindAnyObjectByType<Item_interaction>();
        // 확률에 따라 아이템 드랍 결정
        if (Random.value <= dropChance)
        {
            // 배열에서 랜덤한 아이템 선택
            int randomIndex = Random.Range(0, Weapon_drop_prefeb.Length);

            // 선택된 아이템 프리팹을 현재 위치에 생성
            GameObject newItem = Instantiate(Weapon_drop_prefeb[randomIndex], item_interaction.currentBox.transform.position, Quaternion.identity);
            Debug.Log(Weapon_drop_prefeb[randomIndex].name + "떨굼");

            // 아이템을 상자 위로 올리기 위한 애니메이션 코루틴 시작
            StartCoroutine(ItemRiseAndFall(newItem));
        }
    }

    private IEnumerator ItemRiseAndFall(GameObject item)
    {
        float initialHeight = item.transform.position.y; // 아이템이 생성된 초기 높이
        float targetHeight = initialHeight + 1f; // 아이템이 올라갈 목표 높이 (예: 1.5f)

        // 아이템을 상자 위로 올리는 애니메이션
        while (item.transform.position.y < targetHeight)
        {
            // 아이템을 위로 올림
            item.transform.Translate(Vector3.up * Time.deltaTime, Space.World);
            yield return null;
        }

        // 일정 시간(예: 1초) 대기
        yield return new WaitForSeconds(0.7f);

        // 아이템을 다시 아래로 내리는 애니메이션
        while (item.transform.position.y > initialHeight)
        {
            // 아이템을 아래로 내림
            item.transform.Translate(Vector3.down * Time.deltaTime, Space.World);
            yield return null;
        }

        // 아이템이 다시 원래 위치에 도달했을 때, 아이템을 삭제합니다.
        //Destroy(item);
    }

}
