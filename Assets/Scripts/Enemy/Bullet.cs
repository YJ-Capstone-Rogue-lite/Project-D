using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Weapon weapon;

    private float speed; //탄속


    private float lifeTime; //총알이 맵에 머물러있는 시간(사거리)

    private Rigidbody2D rb; //리지드바디




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
        Destroy(gameObject, lifeTime);
    }

    private void FixedUpdate()
    {
        rb.velocity = transform.up * speed;
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player")) //Bullet에 접촉했을 때
        {
            Destroy(gameObject);
            Debug.Log("총을을 지움!");
        }
        }
    }
