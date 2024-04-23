using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Item_Manager : ScriptableObject
{
    public enum ItemType
    {
        Weapon,
        Accessory,
        Potion  
    }


    public ItemType itemType;

    [System.Serializable]
    public struct STAT
    {
        //무기 스탯
        //무기 이름
        public string Name;
        
        //무기 등급
        public int Grade;

        //탄속
        public float bullet_speed;

        //발사 속도
        public float Fire_Rate;

        //데미지
        public float damage;

        //발사시 화면 흔들림 효과
        public float FireShakeForce;

        //무기 설명
        public string weapon_tooltip;

        //총알 두께
        public float SkinWidth;

    }

    public List<STAT> stats = new List<STAT>();

}

