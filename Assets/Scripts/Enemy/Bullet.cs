using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Weapon weapon;

    private float speed; //ź��


    private float lifeTime; //�Ѿ��� �ʿ� �ӹ����ִ� �ð�(��Ÿ�)

    private Rigidbody2D rb; //������ٵ�




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
        if (other.gameObject.CompareTag("Player")) //Bullet�� �������� ��
        {
            Destroy(gameObject);
            Debug.Log("������ ����!");
        }
        }
    }
