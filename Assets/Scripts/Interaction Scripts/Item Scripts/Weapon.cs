using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Weapon", menuName = "Scriptable Object/WeaponData")]

public class Weapon : ScriptableObject
{
    public enum RatingType { NONE, COMMON, RARE, UNIQUE, LEGENDARY } //아이템 등급

    public enum WeaponType { Pistol, Assaultt_Rifle, Sniper_Rifle } //무기 타입

    [Header(" # 아이템 기본 정보")]
    public int number; //아이템 번호
    public string name; //아이템 이름
    public Sprite img; //아이템 이미지
    public string info; //아이템 정보(설명) 
    public RatingType ratingType; //아이템 등급
    public WeaponType weaponType; // 무기타입
    public float bullet_range; //사거리
    public float bullet_velocity; //탄속
    public float Fire_rate; //연사속도
    public float Damage; // 데미지

    
}
