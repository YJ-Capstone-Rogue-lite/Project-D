using BehaviourTree;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyBullet : MonoBehaviour
{
    public float bullet_speed;
    public Rigidbody2D rb;
    public Vector2 direction;
    public Transform playerPos;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Rigidbody2D 컴포넌트를 가져옴
        playerPos = GameObject.FindWithTag("Player").GetComponent<Transform>(); // "Player" 태그를 가진 게임 오브젝트를 찾음
        Vector2 direction = (playerPos.position - transform.position).normalized;
        rb.velocity = direction * bullet_speed;

        Destroy(gameObject, 5f);
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log(other.gameObject);
        if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Wall"))
        {
            Destroy(gameObject);
            Debug.Log("총알을 지움!"+ other.gameObject.name);
        }
    }
}
