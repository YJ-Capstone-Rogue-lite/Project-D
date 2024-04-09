using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Weapon weapon;
    
    public Transform target;
    public float speed = 3f;
    public float rotateSpeed = 0.25f;
    public Rigidbody2D rb;
    public float enemy_hp = 100;

    private void Start()
    {
         rb = GetComponent<Rigidbody2D>();

    }
    private void Update()
    {
        if (!target)
        {
            GetTarget();
        }
        else
        {
            RotateTowardTarget();
        }

        Enemy_die();

    }
    private void FixedUpdate()
    {
        rb.velocity = transform.up * speed;
    }

    private void RotateTowardTarget()
    {
        Vector2 targetDirection = target.position - transform.position;
        float angle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg - 90f;
        Quaternion q = Quaternion.Euler(new Vector3(0,0,angle));
        transform.localRotation = Quaternion.Slerp(transform.localRotation, q, rotateSpeed);
    }

    private void GetTarget() //플레이어 위치 찾기
    {
        if (GameObject.FindGameObjectWithTag("Player"))
        {
            target = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Bullet")) //Bullet에 접촉했을 때
        {
            enemy_hp -= weapon.Damage;
        }
    }

    private void Enemy_die()
    {
        if (enemy_hp <= 0)
        {
            Destroy(gameObject);
        }
    }
}
