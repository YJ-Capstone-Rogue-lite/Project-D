using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Weapon weapon;
    private Fire_Test fire_test;

    private float speed; //탄속

    private float lifeTime; //총알이 맵에 머물러있는 시간(사거리)

    private Rigidbody2D rb; //리지드바디

    private CharStateData charStateData;

    [SerializeField]private Character character;

    public float Damage { get; private set; }


    //해당 코드를 데미지 관련해서 쓰고있긴한데 바꾸거나 정리해야할듯
 
    public void setup(Weapon weapon)
    {
        rb = GetComponent<Rigidbody2D>();
        this.weapon = weapon;

        speed = weapon.bullet_velocity;
        lifeTime = weapon.bullet_range;


        Damage = weapon.Damage;

        
        // Calculate damage with character stats
        //Damage = weapon.Damage + (character != null ? character.m_damage + character.m_buff_damage : 0);
        Destroy(gameObject, lifeTime);
    }

    private void FixedUpdate()
    {
        rb.velocity = transform.up * speed;
        
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Enemy") && (weapon.weaponType != Weapon.WeaponType.Sniper_Rifle) || other.gameObject.CompareTag("Boss")) //Enemy에 접촉하고 아이템 타입이 스나이퍼가 아닌 경우에
        {
            Destroy(gameObject);

        }

        else if(other.gameObject.CompareTag("Wall") || other.gameObject.CompareTag("NPC") || other.gameObject.CompareTag("Object")) //Wall에 접촉하고 아이템 타입이 스나이퍼가인 경우에
        {
            Destroy(gameObject);

        }
        if (weapon.weaponType == Weapon.WeaponType.Sniper_Rifle && other.gameObject.CompareTag("Enemy"))
        {
            rb.isKinematic = true;
        }
        else
        {
            rb.isKinematic = false;
        }
    }
    private void OnCollisionExit2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Enemy") && weapon.weaponType == Weapon.WeaponType.Sniper_Rifle)
        {
            rb.isKinematic = false;
        }
    }
}