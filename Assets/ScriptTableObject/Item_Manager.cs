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
        //���� ����
        //���� �̸�
        public string Name;
        
        //���� ���
        public int Grade;

        //ź��
        public float bullet_speed;

        //�߻� �ӵ�
        public float Fire_Rate;

        //������
        public float damage;

        //�߻�� ȭ�� ��鸲 ȿ��
        public float FireShakeForce;

        //���� ����
        public string weapon_tooltip;

        //�Ѿ� �β�
        public float SkinWidth;

    }

    public List<STAT> stats = new List<STAT>();

}

