using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Weapon", menuName = "Scriptable Object/WeaponData")]

public class Weapon : ScriptableObject
{
    public enum RatingType { NONE, COMMON, RARE, UNIQUE, LEGENDARY } //아이템 등급

    public enum WeaponType { Pistol, Assaultt_Rifle, Sniper_Rifle, Shoot_Gun } //무기 타입

    [Header(" # 아이템 기본 정보")]
    public RatingType ratingType; //아이템 등급
    public WeaponType weaponType; // 무기타입

    public int number; //아이템 번호
    public string name; //아이템 이름
    public Sprite sprite; //아이템 스프라이트
    public float bullet_range; //사거리
    public float bullet_velocity; //탄속
    public float Fire_rate; //연사속도
    public float reload_time;//장전속도
    public float magazine_capacity; //장탄수
    public float backup_magazine_capacity; //재장전시 장탄수 저장용

    public float Damage; // 데미지

    [TextArea(3, 10)] // 첫 번째 매개변수는 텍스트 입력창의 높이를 결정하고, 두 번째 매개변수는 텍스트 입력창의 너비를 결정합니다.
    public string info; //아이템 정보(설명) 
}