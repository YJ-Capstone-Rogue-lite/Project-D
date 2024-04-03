using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Weapon", menuName = "Scriptable Object/WeaponData")]

public class Weapon : ScriptableObject
{
    public enum RatingType { NONE, COMMON, RARE, UNIQUE, LEGENDARY } //������ ���

    public enum WeaponType { Pistol, Assaultt_Rifle, Sniper_Rifle } //���� Ÿ��

    [Header(" # ������ �⺻ ����")]
    public int number; //������ ��ȣ
    public string name; //������ �̸�
    public Sprite img; //������ �̹���
    public string info; //������ ����(����) 
    public RatingType ratingType; //������ ���
    public WeaponType weaponType; // ����Ÿ��
    public float bullet_range; //��Ÿ�
    public float bullet_velocity; //ź��
    public float Fire_rate; //����ӵ�
    public float Damage; // ������

    
}
