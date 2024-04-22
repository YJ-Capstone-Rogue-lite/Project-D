<<<<<<< HEAD
using System.Collections;
=======
﻿using System.Collections;
>>>>>>> System
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
<<<<<<< HEAD
    private Weapon weapon;
    private FIre_Test fire_test;

    private float speed; //탄속

=======
    public Weapon weapon;

    private float speed; //탄속


>>>>>>> System
    private float lifeTime; //총알이 맵에 머물러있는 시간(사거리)

    private Rigidbody2D rb; //리지드바디

<<<<<<< HEAD
    public float Damage { get; private set; }



    public void setup(Weapon weapon)
    {
        rb = GetComponent<Rigidbody2D>();
        this.weapon = weapon;
        speed = weapon.bullet_velocity;
        lifeTime = weapon.bullet_range;
        Damage = weapon.Damage;
=======



    //private void Start()
    //{
    //    rb = GetComponent<Rigidbody2D>();
    //    setup();
    //    Destroy(gameObject, lifeTime);


    //}

    public void setup(Weapon weapon)
    {
        rb = GetComponent<Rigidbody2D>();
        speed = weapon.bullet_velocity;
        lifeTime = weapon.bullet_range;
>>>>>>> System
        Destroy(gameObject, lifeTime);
    }

    private void FixedUpdate()
    {
        rb.velocity = transform.up * speed;
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
<<<<<<< HEAD
        if (other.gameObject.CompareTag("Enemy") && (weapon.weaponType != Weapon.WeaponType.Sniper_Rifle)) //Enemy에 접촉하고 아이템 타입이 스나이퍼가 아닌 경우에
        {
            Destroy(gameObject);
            Debug.Log("총알을 지움!");

        }

        else if(other.gameObject.CompareTag("Wall") && (weapon.weaponType == Weapon.WeaponType.Sniper_Rifle)) //Wall에 접촉하고 아이템 타입이 스나이퍼가인 경우에
        {
            Destroy(gameObject);
            Debug.Log("총알을 지움!");

        }
    }
}
=======
        if (other.gameObject.CompareTag("Player")) //Bullet에 접촉했을 때
        {
            Destroy(gameObject);
            Debug.Log("총을을 지움!");
        }
        }
    }
>>>>>>> System
