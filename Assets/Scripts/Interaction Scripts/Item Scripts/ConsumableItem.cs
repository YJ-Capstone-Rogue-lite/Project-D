using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ConsumableItem", menuName = "Scriptable Object/ConsumableItemData")]
public class ConsumableItem : ScriptableObject
{
    public GameObject ConsumItemPrefab;//해당 소비템 값을 가지고있는 프리팹

    public enum RatingType { NONE, COMMON, RARE, UNIQUE, LEGENDARY } //아이템 등급
    public enum ItemType { None, Potion, Throwing_Weapon } //아이템 타입(None, 포션, 투척무기)

    [Header(" # 아이템 기본 정보")]
    public RatingType ratingType; //아이템 등급
    public ItemType itemType; // 아이템 타입
    public int number; //아이템 번호
    public string name; //아이템 이름
    public Sprite sprite; //아이템 스프라이트

    [Header(" # 포션류 기본 정보")]    //이후로 기본적으로 넣을 효과들 추가해야함(hp 증가나 이속 증가, 탄속 증가 등)   
    public float HPHealing; //hp회복 값
    public float RemoveToxic; //중독 디버프 해제 값
    public float AddShield; //쉴드증가 값(악세사리 AddShield와 동일 효과)
    //public float Removerupture; //파괴 스택 제거

    [Header(" # 투척무기 기본 정보")]
    public float Throwing_Weapon_Damage;//투척 무기 데미지
    public float Throwing_Weapon_Range;//투척 무기 범위

    [Header(" # 아이템 설명")]
    [TextArea(3, 10)] // 첫 번째 매개변수는 텍스트 입력창의 높이를 결정하고, 두 번째 매개변수는 텍스트 입력창의 너비를 결정합니다.
    public string info; //아이템 정보(설명) 

}
