using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_drop : MonoBehaviour
{
    public static Item_drop single;
    public GameObject[] ConsumableItem_drop_prefeb; //드랍될 소비아이템을 저장해둔 배열
    public GameObject[] Weapon_drop_prefeb; //드랍될 무기를 저장해둔 배열

    public float dropChance = 1f; // 아이템 드랍 확률 (0.5는 50%)
    public Item_interaction item_interaction;

    public float floatSpeed = 4f; // 위아래로 움직이는 속도
    public float floatAmplitude = 0.03f; // 움직이는 범위
    public float floatingDuration = 1.0f; // 위아래로 움직이는 시간
    public float delayBeforeFloating = 0.7f; // 움직이기 전 대기 시간

    private void Awake()
    {
        item_interaction = FindAnyObjectByType<Item_interaction>();

    }

    //몬스터가 죽으면 일정 확률로 포션 또는 소비아이템 드랍
    public void enemy_item_drop()
    {
        // 확률에 따라 아이템 드랍 결정
        if (Random.value <= dropChance)
        {
            // 배열에서 랜덤한 아이템 선택
            int randomIndex = Random.Range(0, ConsumableItem_drop_prefeb.Length);

            // 선택된 아이템 프리팹을 현재 위치에 생성
            GameObject newItem = Instantiate(ConsumableItem_drop_prefeb[randomIndex], gameObject.transform.position, Quaternion.identity);
            Debug.Log(ConsumableItem_drop_prefeb[randomIndex].name + "떨굼");

            // 아이템을 상자 위로 올리기 위한 애니메이션 코루틴 시작
            //StartCoroutine(ItemRiseAndFall(newItem));
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
            Debug.Log("AAA : " + Weapon_drop_prefeb.Length + ", " + randomIndex);

            // 선택된 아이템 프리팹을 현재 위치에 생성
            GameObject newWeapon = Instantiate(Weapon_drop_prefeb[randomIndex], item_interaction.currentBox.transform.position, Quaternion.identity);
            Debug.Log(Weapon_drop_prefeb[randomIndex].name + "떨굼");

            // 아이템을 상자 위로 올리기 위한 애니메이션 코루틴 시작
            StartCoroutine(ItemRiseAndFall(newWeapon));
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

        // 일정 시간 대기
        yield return new WaitForSeconds(delayBeforeFloating);

        // 아이템을 다시 아래로 내리는 애니메이션
        while (item.transform.position.y > initialHeight)
        {
            // 아이템을 아래로 내림
            item.transform.Translate(Vector3.down * Time.deltaTime, Space.World);
            yield return null;
        }

        // 일정 시간 대기
        yield return new WaitForSeconds(delayBeforeFloating);

        // 위아래로 둥실둥실 움직이는 애니메이션
        float startTime = Time.time; // 시작 시간 저장
        while (true)
        {
            // 위아래로 둥실둥실 움직이는 애니메이션
            float newY = initialHeight + Mathf.Sin((Time.time - startTime) * floatSpeed) * floatAmplitude;
            item.transform.position = new Vector3(item.transform.position.x, newY, item.transform.position.z);
            yield return null;
        }
    }
}