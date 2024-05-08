using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Accessory", menuName = "Scriptable Object/AccessoryData")]
public class Accessory : ScriptableObject
{
    public enum RatingType { NONE, COMMON, RARE, UNIQUE, LEGENDARY } //아이템 등급
    public enum ItemType { None, Accessory } //아이템 타입(None, 악세서리)
    
    [Header(" # 아이템 기본 정보")]
    public RatingType ratingType; //아이템 등급
    public ItemType itemType; // 아이템 타입
    public int number; //아이템 번호
    public string name; //아이템 이름
    public Sprite sprite; //아이템 스프라이트
    
    [Header(" # 악세사리 기본 정보")]    //이후로 악세사리 효과들 추가해야함(hp 증가나 이속 증가, 탄속 증가 등)   
    public float AddMaxHP; //hp최대증가 값
    public float AddHP; //hp증가 값
    public float AddMoveSpeed; //이동속도 증가 값
    public float AddShield; //쉴드 증가 값
    public float AddWeaponDamage;//무기 데미지 증가
    public float reduce_reload_time;//무기 장전속도 감소

    [Header(" # 아이템 설명")]
    [TextArea(3, 10)] // 첫 번째 매개변수는 텍스트 입력창의 높이를 결정하고, 두 번째 매개변수는 텍스트 입력창의 너비를 결정합니다.
    public string info; //아이템 정보(설명) 


}

